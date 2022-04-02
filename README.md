# Assesment

Added local DB, Named InventoryDB, If unable to attach DB to local sql DB, 

Just create add new database in SQL server explorer name InventoryDB, and execute create table script.sql

In unit test project, Added initial data to local Db by reading JSON object, Created Mock Json for products using https://www.mockaroo.com/.

Added swagger for API

- Inventory API.

Product with already sold status cannot be updated.

Product with damaged status cannot be sold.

Not sure whether I need to generate barcode image in save db and id should be mapped to product table.

And API's Product and Category is added for reference.
