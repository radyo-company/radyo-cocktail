BEGIN TRANSACTION;

CREATE UNIQUE INDEX "IX_Cocktail_Name" ON "Cocktail" ("Name");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240320104300_UniqueCocktailName', '8.0.2');

COMMIT;

