// when there are links between product and lead
WITH '250' AS q
OPTIONAL MATCH (p:Product), (l:Lead)
WHERE ((p)--(l) OR (p.value <= toFloat(q) OR toFloat(q) >= p.value))
WITH p, l
RETURN p, l
ORDER BY p.value ASC

// when there are no links between product and lead
WITH '115.0' AS q
OPTIONAL MATCH (p:Product)
WHERE (p.value <= toFloat(q) OR toFloat(q) >= p.value)
WITH p
RETURN p
ORDER BY p.value ASC