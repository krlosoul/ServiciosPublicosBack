INSERT INTO [User] (name, email) VALUES('Carlos','krlosoul0@gmail.com');
INSERT INTO [User] (name, email) VALUES('Cenhawer','krlosoul1@gmail.com');
INSERT INTO [User] (name, email) VALUES('krlos','krlosoul2@gmail.com');

INSERT INTO [Role] (name) VALUES ('Administrador');
INSERT INTO [Role] (name) VALUES ('Editor');
INSERT INTO [Role] (name) VALUES ('Visualizador');

INSERT INTO UserRole VALUES (1,1);
INSERT INTO UserRole VALUES (2,2);
INSERT INTO UserRole VALUES (3,3);

INSERT INTO Service (Name) VALUES('Agua');
INSERT INTO Service (Name) VALUES('Gas');
INSERT INTO Service (Name) VALUES('Energia');
INSERT INTO Service (Name) VALUES('Internet');

INSERT INTO Status (Name) VALUES('Creado');
INSERT INTO Status (Name) VALUES('Pendiente');
INSERT INTO Status (Name) VALUES('En Proceso');
INSERT INTO Status (Name) VALUES('Finalizado');