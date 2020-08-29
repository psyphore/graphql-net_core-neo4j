using System.Collections.Generic;

namespace DataAccess.Search
{
    public class SearchQueries
    {
        public IDictionary<string, string> Queries => new Dictionary<string, string>
                {
                    { "SEARCH", @"
                        WITH $query AS query
                        OPTIONAL MATCH (p:Person), (b:Building), (pr:Product)
                        WHERE (toLower(p.title) CONTAINS toLower(query) 
		                        OR toLower(p.firstname) CONTAINS toLower(query) 
                                OR toLower(p.lastname) CONTAINS toLower(query)
                                OR query CONTAINS "" "" AND (toLower(query) = toLower(p.firstname)+ "" ""+ toLower(p.lastname))
                                OR query CONTAINS "", "" AND (toLower(query) = toLower(p.lastname)+ "", ""+ toLower(p.firstname))
                                )

                            OR ((p)--(b) AND (toLower(b.name) CONTAINS toLower(query) OR toLower(b.address) CONTAINS toLower(query)))

                            OR ((p)--(pr) AND (toLower(pr.name) CONTAINS toLower(query)))
                        WITH apoc.date.format(p.deactivated, 'yyyyMMdd HH:mm:ss.ms') AS deactivated, p
                        WITH p { 
                            .firstname,
                            .mobile,
                            .bio,
                            .id,
                            .title,
                            .email,
                            .lastname,
                            .avatar,
                            .knownAs,
                            deactivated: deactivated
                            } AS person
                        RETURN DISTINCT person
                        ORDER BY person.lastname ASC, person.firstname ASC
                        SKIP $offset
                        LIMIT $first
                    "},
                    { "ADVANCED_SEARCH", @"
                        WITH {query} AS query
                        OPTIONAL MATCH (p:Person), (b:Building), (pr:Product)
                        WHERE   p.title =~ ""(?i).*$query.*""
                                OR p.firstname =~ ""(?i)$query.*""
                                OR p.lastname =~ ""(?i)$query.*""
                                OR query CONTAINS "" "" AND (toLower(query) = toLower(p.firstname)+ "" ""+ toLower(p.lastname))
                                OR query CONTAINS "", "" AND (toLower(query) = toLower(p.lastname)+ "", ""+ toLower(p.firstname))

                            OR ((p)--(b) AND (toLower(b.name) CONTAINS toLower(query) OR toLower(b.address) CONTAINS toLower(query)))
                            OR ((p)--(pr) AND (toLower(pr.name) CONTAINS toLower(query)))
                        WITH apoc.date.format(p.deactivated, 'yyyyMMdd HH:mm:ss.ms') AS deactivated, p
                        WITH p { 
                            .firstname,
                            .mobile,
                            .bio,
                            .id,
                            .title,
                            .email,
                            .lastname,
                            .avatar,
                            .knownAs,
                            deactivated: deactivated
                            } AS person
                        RETURN DISTINCT person
                        ORDER BY person.lastname ASC, person.firstname ASC
                        SKIP $offset
                        LIMIT $first
                    " }
                };
    }
}