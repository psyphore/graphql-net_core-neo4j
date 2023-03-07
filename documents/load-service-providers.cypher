CREATE DATABASE Leads;
// load service Products
// This is an initialization script for the leads graph.
// Run it only once. 😊
// Have you run it twice? Use `MATCH (n) WHERE (n:Lead OR n:Product) DETACH DELETE n` to start over.
CREATE (Product1:Product {id: apoc.create.uuid(), value: 80.0, name:'Gigabyte GA-X670 AOSUS-ELITE-AX', tags: ['motherboard', 'AMD', 'AM5']}),
  (Product2:Product {id: apoc.create.uuid(), value: 110.0, name:'MSI MEG X670E ACE', tags: ['motherboard', 'AMD', 'AM5']}),
  (Product3:Product {id: apoc.create.uuid(), value: 115.0, name:'ASUS ROG STRIX B650E-F GAMING WIFI', tags: ['motherboard', 'AMD', 'AM5']}),
  
  (Product4:Product {id: apoc.create.uuid(), value: 65.0, name:'ASUS PRIME B660M-K D4', tags: ['motherboard', 'Intel', 'LGA 1700']}),
  (Product5:Product {id: apoc.create.uuid(), value: 190.0, name:'MSI MAG Z690 TOMAHAWK WIFI', tags: ['motherboard', 'Intel', 'LGA 1700']}),
  (Product6:Product {id: apoc.create.uuid(), value: 205.0, name:'ASRock Z790 Steel Legend WIFI 6', tags: ['motherboard', 'Intel', 'LGA 1700']}),
  
  (Product7:Product {id: apoc.create.uuid(), value: 300.0, name:'Intel Arc A750 8GB GDDR6 PCIE 4.0', tags: ['GPU', 'Intel']}),
  (Product8:Product {id: apoc.create.uuid(), value: 125.0, name:'XFX Radeon RX 7900 XTX Merc 310 Black Speedster 24GB', tags: ['GPU', 'AMD']}),
  (Product9:Product {id: apoc.create.uuid(), value: 235.0, name:'MSI GeForce RTX 4090 SUPRIM LIQUID X 24GB', tags: ['GPU', 'NVIDIA']}),

  (Product10:Product {id: apoc.create.uuid(), value: 305.0, name:'ASUS ROG Thor 1600W Titanium', tags: ['PSU', '80+ Titanium', '1600W']})
;