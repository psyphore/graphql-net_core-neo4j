using System.Collections.Generic;

namespace DataAccess.Person
{
    public class PersonQueries
    {
        public IDictionary<string, string> Mutations
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "UPDATE_PERSON", @"
                        MERGE (p:Person{id: $id})
                        SET p += {
                          title: $title
                          , firstname: $firstname
                          , lastname: $lastname
                          , mobile: $mobile
                          , email: $email
                          , avatar: $avatar
                          , bio: $bio
                          , subscriptions: $subscriptions
                        }
                        RETURN p AS person;
                    "},
                    { "UPDATE_PERSON_2", @"
                        MERGE (p:Person{id: $id})
                        SET p += {
                          mobile: $mobile
                          , email: $email
                          , bio: $bio
                          , knownAs: $knownAs
                        }
                        RETURN p AS person;
                    "},
                    { "UPDATE_PERSON_REPORTING", @"
                        MATCH (m:Person{id:$man}-[r:MANAGES]->(s:Person{id:$sub}))
                        MERGE (m)-[r2:MANAGES]->(s)
                        WITH r, s, m, r2
                        DELETE r
                        RETURN s
                    "},
                    { "DEACTIVATE_PERSON", @"
                        MATCH (p:Person) 
                        WHERE p.id = $id
                        SET p.deactivated = timestamp()
                        RETURN p
                    " }
                };
            }
        }

        public IDictionary<string, string> Queries
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "GET_PERSON", @"
                        OPTIONAL MATCH (p:Person{id: {id}})
                        WITH apoc.date.format(p.deactivated, 'yyyyMMdd HH:mm:ss.ms') AS deactivated, p
                        RETURN p { 
                        .firstname,
                        .mobile,
                        .bio,
                        .id,
                        .title,
                        .email,
                        .lastname,
                        .avatar,
                        .knownAs,
                        manager: apoc.cypher.runFirstColumn(""MATCH (m)-[:MANAGES]->(this) RETURN m LIMIT 1"", {this: p}, false),
                        team: [(p)<-[:MANAGES]-()-[:MANAGES]->(t) | t],
                        line: [(s) < -[:MANAGES] - (p) | s],
                        products: [(p) -[:KNOWS]->(pr) | pr],
                        building: [(p) -[:BASED_IN]->(b) | b],
                        deactivated: deactivated
                        } AS person
                    "},
                    { "GET_PEOPLE", @"
                        MATCH (p:Person)
                        WITH apoc.date.format(p.deactivated, 'dd/MM/yyyy HH:mm:ss') AS deactivated, p
                        RETURN p { 
                          .firstname,
                          .mobile,
                          .bio,
                          .id,
                          .title,
                          .email,
                          .lastname,
                          .avatar,
                          .knownAs,
                          manager: apoc.cypher.runFirstColumn(""MATCH (m)-[:MANAGES]->(this) RETURN m LIMIT 1"", {this: p}, false),
                          team: [(p)<-[:MANAGES]-()-[:MANAGES]->(t) | t],
                          line: [(s)<-[:MANAGES]-(p) | s],
                          /*products: [(p)-[:KNOWS]->(pr) | pr],
                          building: [(p)-[:BASED_IN]->(b) | b],*/
                          deactivated: deactivated
                        } AS person
                        ORDER BY person.lastname ASC, person.firstname ASC
                        SKIP {offset}
                        LIMIT {first}
                    " },
                    { "GET_ME", @"
                        MATCH (p:Person)
                        WHERE LOWER($firstname) CONTAINS LOWER(p.firstname) AND
                                LOWER($lastname) CONTAINS LOWER(p.lastname) AND
                                LOWER($email) CONTAINS LOWER(p.email)
                        WITH apoc.date.format(p.deactivated, 'yyyyMMdd HH:mm:ss.ms') AS deactivated, p
                        RETURN p { 
                        .firstname,
                        .mobile,
                        .bio,
                        .id,
                        .title,
                        .email,
                        .lastname,
                        .avatar,
                        .knownAs,
                        manager: apoc.cypher.runFirstColumn(""MATCH (m)-[:MANAGES]->(this) RETURN m LIMIT 1"", {this: p}, false),
                        team: [(p)<-[:MANAGES]-()-[:MANAGES]->(t) | t],
                        line: [(s) < -[:MANAGES] - (p) | s],
                        products: [(p) -[:KNOWS]->(pr) | pr],
                        building: [(p) -[:BASED_IN]->(b) | b],
                        subscriptions: ['meals', 'support', 'leave', 'general'],
                        deactivated: deactivated
                        } AS person
                    " },
                    { "GET_FUZZY_PERSON", @"
                        OPTIONAL MATCH (p:Person)
                        WHERE LOWER($firstname) CONTAINS LOWER(p.firstname) AND
                              LOWER($lastname) CONTAINS LOWER(p.lastname)
                        RETURN p AS person
                    " },
                    { "PERSONAL_NOTES", @"
                        WITH apoc.create.uuid() AS uuid 
                        WITH {id: uuid, subject: 'Notification', body: 'test note'} AS note
                        RETURN [note] AS notes
                    " }
                };
            }
        }
    }
}