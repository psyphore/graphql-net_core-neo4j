// when there are no links between product and lead
WITH '115.0' AS q
OPTIONAL MATCH (p:Product)<--(m:Money { active: true })
WHERE (m.amount <= toFloat(q) AND m.amount >= toFloat(q))
RETURN p { .*, price: m{ .currency, .amount } }
ORDER BY m.amount ASC