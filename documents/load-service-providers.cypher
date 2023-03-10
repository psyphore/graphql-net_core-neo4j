// DROP Leads;
// CREATE DATABASE Leads;
// load service Products
// This is an initialization script for the leads graph.
// Run it only once. 😊
// Have you run it twice? Use `MATCH (n:Product|Money|Address|Lead|Order|Tag) DETACH DELETE n` to start over.
CREATE 
  (Product1:Product {id: apoc.create.uuid(), name:'Gigabyte GA-X670 AOSUS-ELITE-AX', sku: apoc.text.random(15, 'A-Z0-9') }),
  (Product2:Product {id: apoc.create.uuid(), name:'MSI MEG X670E ACE', sku: apoc.text.random(15, 'A-Z0-9') }),
  (Product3:Product {id: apoc.create.uuid(), name:'ASUS ROG STRIX B650E-F GAMING WIFI', sku: apoc.text.random(15, 'A-Z0-9') }),
  (Product4:Product {id: apoc.create.uuid(), name:'ASUS PRIME B660M-K D4', sku: apoc.text.random(15, 'A-Z0-9') }),
  (Product5:Product {id: apoc.create.uuid(), name:'MSI MAG Z690 TOMAHAWK WIFI', sku: apoc.text.random(15, 'A-Z0-9') }),
  (Product6:Product {id: apoc.create.uuid(), name:'ASRock Z790 Steel Legend WIFI 6', sku: apoc.text.random(15, 'A-Z0-9') }),
  (Product7:Product {id: apoc.create.uuid(), name:'Intel Arc A750 8GB GDDR6 PCIE 4.0', sku: apoc.text.random(15, 'A-Z0-9') }),
  (Product8:Product {id: apoc.create.uuid(), name:'XFX Radeon RX 7900 XTX Merc 310 Black Speedster 24GB', sku: apoc.text.random(15, 'A-Z0-9') }),
  (Product9:Product {id: apoc.create.uuid(), name:'MSI GeForce RTX 4090 SUPRIM LIQUID X 24GB', sku: apoc.text.random(15, 'A-Z0-9') }),
  (Product10:Product {id: apoc.create.uuid(), name:'ASUS ROG Thor 1600W Titanium', sku: apoc.text.random(15, 'A-Z0-9') })

CREATE 
  (T1:Tag {value: 'Montherboard'}),
  (T2:Tag {value: 'GPU'}),
  (T3:Tag {value: 'PSU'}),
  (T4:Tag {value: 'Intel'}),
  (T5:Tag {value: 'AMD'}),
  (T6:Tag {value: 'NVIDIA'}),
  (T7:Tag {value: 'AM5'}),
  (T8:Tag {value: 'LGA 1700'}),
  (T9:Tag {value: '80+ Titanium'}),
  (T10:Tag {value: '1600W'})

CREATE
  (Product1)<-[:HAS_TAG]-(T1),
  (Product1)<-[:HAS_TAG]-(T5),
  (Product1)<-[:HAS_TAG]-(T7),
  
  (Product2)<-[:HAS_TAG]-(T1),
  (Product2)<-[:HAS_TAG]-(T5),
  (Product2)<-[:HAS_TAG]-(T7),
  
  (Product3)<-[:HAS_TAG]-(T1),
  (Product3)<-[:HAS_TAG]-(T5),
  (Product3)<-[:HAS_TAG]-(T7),
  
  (Product4)<-[:HAS_TAG]-(T1),
  (Product4)<-[:HAS_TAG]-(T4),
  (Product4)<-[:HAS_TAG]-(T8),
  
  (Product5)<-[:HAS_TAG]-(T1),
  (Product5)<-[:HAS_TAG]-(T4),
  (Product5)<-[:HAS_TAG]-(T8),
  
  (Product6)<-[:HAS_TAG]-(T1),
  (Product6)<-[:HAS_TAG]-(T4),
  (Product6)<-[:HAS_TAG]-(T8),
  
  (Product7)<-[:HAS_TAG]-(T2),
  (Product7)<-[:HAS_TAG]-(T4),
  
  (Product8)<-[:HAS_TAG]-(T2),
  (Product8)<-[:HAS_TAG]-(T5),
  
  (Product9)<-[:HAS_TAG]-(T2),
  (Product9)<-[:HAS_TAG]-(T6),
  
  (Product10)<-[:HAS_TAG]-(T3),
  (Product10)<-[:HAS_TAG]-(T9),
  (Product10)<-[:HAS_TAG]-(T10)
  
CREATE
  (M1:Money { active: true, updated: timestamp(), updatedBy: 'system', currency: 'ZAR', amount: 80.0}),
  (M2:Money { active: true, updated: timestamp(), updatedBy: 'system', currency: 'ZAR', amount: 115.0}),
  (M3:Money { active: true, updated: timestamp(), updatedBy: 'system', currency: 'ZAR', amount: 115.0}),
  (M4:Money { active: true, updated: timestamp(), updatedBy: 'system', currency: 'ZAR', amount: 65.0 }),
  (M5:Money { active: true, updated: timestamp(), updatedBy: 'system', currency: 'ZAR', amount: 190.0 }),
  (M6:Money { active: true, updated: timestamp(), updatedBy: 'system', currency: 'ZAR', amount: 205.0 }),
  (M7:Money { active: true, updated: timestamp(), updatedBy: 'system', currency: 'ZAR', amount: 300.0 }),
  (M8:Money { active: true, updated: timestamp(), updatedBy: 'system', currency: 'ZAR', amount: 152.0 }),
  (M9:Money { active: true, updated: timestamp(), updatedBy: 'system', currency: 'ZAR', amount: 235.0 }),
  (M10:Money { active: true, updated: timestamp(), updatedBy: 'system',currency: 'ZAR', amount: 305.0 })

CREATE
  (Product1)<-[:HAS_PRICE{ created: timestamp(), createdBy: 'system' }]-(M1),
  (Product2)<-[:HAS_PRICE{ created: timestamp(), createdBy: 'system' }]-(M2),
  (Product3)<-[:HAS_PRICE{ created: timestamp(), createdBy: 'system' }]-(M3),
  (Product4)<-[:HAS_PRICE{ created: timestamp(), createdBy: 'system' }]-(M4),
  (Product5)<-[:HAS_PRICE{ created: timestamp(), createdBy: 'system' }]-(M5),
  (Product6)<-[:HAS_PRICE{ created: timestamp(), createdBy: 'system' }]-(M6),
  (Product7)<-[:HAS_PRICE{ created: timestamp(), createdBy: 'system' }]-(M7),
  (Product8)<-[:HAS_PRICE{ created: timestamp(), createdBy: 'system' }]-(M8),
  (Product9)<-[:HAS_PRICE{ created: timestamp(), createdBy: 'system' }]-(M9),
  (Product10)<-[:HAS_PRICE{ created: timestamp(), createdBy: 'system' }]-(M10)
 
CREATE
  (Res:Address{ line1:'55 Acacia Road', line2:'1 Carlswald Meadows Estate', line3: null, suburb:'Blue Hills AH', city:'Midrand', zip:'1685', country:'ZA', created: timestamp()})

CREATE
  (Customer:Lead{ id:apoc.create.uuid(), names:'John Wick', email: 'john.wick@continetal.hightable.org', created: timestamp()})

CREATE
  (Customer)<-[:RESIDES_AT{ since: datetime('2010-11-04T08:00:00'), created: timestamp(), createdBy: 'system' }]-(Res)
  
CREATE TEXT INDEX FOR (n:Product) ON EACH [n.id, n.name, n.tags]
CREATE TEXT INDEX FOR (n:Money) ON (n.currency)
CREATE RANGE INDEX FOR (n:Money) ON (n.amount)
CREATE TEXT INDEX FOR (n:Lead) ON EACH [n.id, n.names]
CREATE TEXT INDEX FOR (n:Address) ON EACH [n.line1, n.line2, n.line3, n.suburb, n.city, n.zip, n.country]

;