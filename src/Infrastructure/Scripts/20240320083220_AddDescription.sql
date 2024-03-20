BEGIN TRANSACTION;

ALTER TABLE "Cocktail" ADD "Description" TEXT NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240320083220_AddDescription', '8.0.2');

COMMIT;

