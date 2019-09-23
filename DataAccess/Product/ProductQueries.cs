using System.Collections.Generic;

namespace DataAccess.Product
{
    public class ProductQueries
    {
        public IDictionary<string, string> Mutations
        {
            get
            {
                return new Dictionary<string, string>
                {
                    { "UPDATE_PRODUCT", @"
                        MERGE (p:Product{id: {id}})
                        SET p += {
                          name: {name}, 
                          description: {description}, 
                          status: {status}
                        }
                        RETURN p AS product;
                    "},
                    { "UPDATE_PRODUCT_KNOWLEDGE", @"
                        MATCH (p:Person{id:{personId}}-[r:KNOWS]->(pr:Product{id:{productId}}))
                        MERGE (p)-[r2:KNOWS]->(pr)
                        WITH r, pr, p, r2
                        DELETE r
                        RETURN pr AS product
                    "},
                    { "UPDATE_PRODUCT_OWNER", @"
                        MATCH (p:Person{id:{personId}}-[r:OWNS]->(pr:Product{id:{productId}}))
                        MERGE (p)-[r2:OWNS]->(pr)
                        WITH r, pr, p, r2
                        DELETE r
                        RETURN pr AS product
                    "},
                    { "DEACTIVATE_PRODUCT", @"
                        MATCH (p:Product) 
                        WHERE p.id = {id}
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
                    { "GET_PRODUCT", @"
                        MATCH (product:Product{id:{id}}) 
                        RETURN product { 
                          .id , 
                          .name , 
                          .description , 
                          .status ,
                          championCount: apoc.cypher.runFirstColumn(""RETURN SIZE((this)<-[:KNOWS]-())"", {this: product}, false),
                          champions: [(product)<-[:KNOWS]-(product_champions:Person) | product_champions]
                        } AS product
                    "},
                    { "GET_PRODUCTS", @"
                        MATCH (product:Product) 
                        RETURN product { 
                          .id , 
                          .name , 
                          .description , 
                          .status ,
                          championCount: apoc.cypher.runFirstColumn(""RETURN SIZE((this)< -[:KNOWS] - ())"", {this: product}, false),
                          champions: [(product) < -[:KNOWS] - (product_champions: Person) | product_champions]
                        } AS product
                        ORDER BY product.name ASC
                        SKIP {offset}
                        LIMIT {first}
                    " }
                };
            }
        }
    }
}