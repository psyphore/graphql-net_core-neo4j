using System.Collections.Generic;

namespace DataAccess.User
{
    public class UserQueries
    {
        public IDictionary<string, string> Mutations => new Dictionary<string, string>
        {
            {
                "UPDATE_USER", @"
                        MERGE (p:User{id: $id})
                        SET p += {
                          firstname: $firstname
                          , email: $email
                          , subscriptions: $subscriptions
                        }
                        RETURN p AS user;
                    "
            },
            {
                "UPDATE_USER_2", @"
                        MERGE (p:User{id: $id})
                        SET p += {
                          email: $email
                        }
                        RETURN p AS user;
                    "
            },
            {
                "UPDATE_USER_REPORTING", @"
                        MATCH (m:User{id:$man}-[r:MANAGES]->(s:User{id:$sub}))
                        MERGE (m)-[r2:MANAGES]->(s)
                        WITH r, s, m, r2
                        DELETE r
                        RETURN s
                    "
            },
            {
                "DEACTIVATE_USER", @"
                        MATCH (p:User) 
                        WHERE p.id = $id
                        SET p.deactivated = timestamp()
                        RETURN p
                    "
            }
        };

        public IDictionary<string, string> Queries => new Dictionary<string, string>
        {
            {
                "GET_USER", @"
                        OPTIONAL MATCH (p:User{id: $id})
                        WITH apoc.date.format(p.deactivated, 'yyyyMMdd HH:mm:ss.ms') AS deactivated, p
                        RETURN p { 
                        .name,
                        .id,
                        .email,
                        deactivated: deactivated
                        } AS user
                    "
            },
            {
                "GET_USERS", @"
                        MATCH (p:User)
                        WITH apoc.date.format(p.deactivated, 'yyyyMMdd HH:mm:ss.ms') AS deactivated, p
                        RETURN p { 
                          .name,
                          .id,
                          .email,
                          .deactivated
                        } AS user
                        ORDER BY user.name ASC
                        SKIP $offset
                        LIMIT $first
                    "
            },
            {
                "GET_ME", @"
                        MATCH (p:User)
                        WHERE LOWER($firstname) CONTAINS LOWER(p.name) AND
                              LOWER($email) CONTAINS LOWER(p.email)
                        WITH apoc.date.format(p.deactivated, 'yyyyMMdd HH:mm:ss.ms') AS deactivated, p
                        RETURN p { 
                        .name,
                        .id,
                        .email
                        deactivated: deactivated
                        } AS user
                    "
            },
            {
                "GET_FUZZY_USER", @"
                        OPTIONAL MATCH (p:User)
                        WHERE LOWER($firstname) CONTAINS LOWER(p.name)
                        RETURN p AS user
                    "
            },
            {
                "PERSONAL_NOTES", @"
                        WITH apoc.create.uuid() AS uuid 
                        WITH {id: uuid, subject: 'Notification', body: 'test note'} AS note
                        RETURN [note] AS notes
                    "
            }
        };
    }
}