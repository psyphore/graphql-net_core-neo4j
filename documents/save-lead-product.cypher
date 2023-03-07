WITH apoc.json.path($lead) AS l0
WITH apoc.map.merge(l0, {createdOn: timestamp()}) AS lu
CALL {
    WITH lu
    CREATE (lead:Lead{id: apoc.create.uuid()})
    SET lead += lu
    RETURN lead
}
CALL {
    OPTIONAL MATCH(product:Product)
    WHERE $productId IS NOT NULL AND p.id = $productId
    RETURN product
}                
CALL {
    WITH lead, p
    WITH [m IN [product] WHERE m IS NOT NULL] AS selections, lead
    UNWIND selections AS selected
    CALL apoc.create.relationship(lead, 'SELECTED', {createdOn: timestamp()}, selected)
    YIELD rel
    RETURN rel AS r1, selected
}
RETURN lead, r1, selected