using DataAccess.Interfaces;
using Models.DTOs.Configuration;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository : IRepository
    {
        private readonly IDriver _driver;
        private readonly string _databaseName;

        public Repository(Connection connection)
        {
            _driver = GraphDatabase.Driver(
                connection.BoltURL,
                AuthTokens.Basic(connection.Username, connection.Password),
                o =>
                {
                    o.WithEncryptionLevel(EncryptionLevel.None);
                });
            _databaseName = connection.DatabaseName;
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }

        public IAsyncSession GetSession(AccessMode mode)
        {
            return _driver.AsyncSession(o => o.WithDatabase(_databaseName));
        }

        public async Task<T> Read<T>(string query, IDictionary<string, object> parameters)
        {
            var x = await ProcessTransactionAsync(GetSession(AccessMode.Read), query, parameters);
            return x.As<T>();
        }

        public async Task<IList<IRecord>> Read(string query, IDictionary<string, object> parameters)
        {
            var records = await ProcessAsync(GetSession(AccessMode.Read), query, parameters);
            return records;
        }

        public async Task<T> Write<T>(string query, object parameters)
        {
            var x = await ProcessWriteTransactionAsync<T>(GetSession(AccessMode.Write), query, parameters);
            return x.As<T>();
        }

        public async Task CreateIndicesAsync(string[] labels)
        {
            if (labels != null && labels.Any())
            {
                var queries = labels.Select(l => string.Format("CREATE INDEX ON :{0}(id)", l)).ToArray();
                var session = GetSession(AccessMode.Write);
                foreach (var query in queries)
                {
                    await session.RunAsync(query);
                }

                await session.CloseAsync();
            }
        }

        private async Task<List<IRecord>> ProcessAsync(IAsyncSession session, string query, IDictionary<string, object> parameters)
        {
            try
            {
                IResultCursor cursor = await session.RunAsync(query, parameters);
                return await cursor.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await session.CloseAsync();
                await _driver.CloseAsync();
            }
        }

        private async Task<List<IRecord>> ProcessTransactionAsync(IAsyncSession session, string query, IDictionary<string, object> parameters)
        {
            try
            {
                var trx = await session.ReadTransactionAsync(tx => tx.RunAsync(query, parameters));
                return await trx.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await session.CloseAsync();
                await _driver.CloseAsync();
            }
        }

        private async Task<List<IRecord>> ProcessWriteTransactionAsync<T>(IAsyncSession session, string query, object parameters)
        {
            try
            {
                var trx = await session.WriteTransactionAsync(tx => tx.RunAsync(query, parameters));
                return await trx.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                await session.CloseAsync();
                await _driver.CloseAsync();
            }
        }
    }
}