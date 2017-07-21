CREATE DATABASE db_dczoem;
USE db_dczoem;

CREATE TABLE area (
	id_area INTEGER NOT NULL AUTO_INCREMENT,
	nombre_area VARCHAR(50) NULL,
	encargado VARCHAR(100) NULL,
	PRIMARY KEY(id_area)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE area_persona (
	id_area INTEGER NOT NULL,
	id_persona VARCHAR(8) NOT NULL,
	PRIMARY KEY(id_area)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE codigo_postal (
	id_codigo INTEGER NOT NULL AUTO_INCREMENT,
	cp INTEGER NULL,
	asentamiento VARCHAR(100) NULL,
	tipo VARCHAR(50) NULL,
	municipio VARCHAR(50) NULL,
	ciudad VARCHAR(50) NULL,
	estado VARCHAR(50) NULL,
	PRIMARY KEY(id_codigo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE equipo (
	id_equipo VARCHAR(8) NOT NULL,
	marca VARCHAR(50) NOT NULL,
	serie VARCHAR(10) NOT NULL,
	descripcion VARCHAR(500) NOT NULL,
	id_tipo_producto INTEGER NOT NULL,
	PRIMARY KEY(id_equipo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE evidencias (
	id_evidencias INTEGER NOT NULL AUTO_INCREMENT,
	num_fotos INTEGER NOT NULL,
	num_videos INTEGER NOT NULL,
	num_cphs INTEGER NOT NULL,
	actualizo VARCHAR(50) NOT NULL,
	fecha TIMESTAMP NOT NULL,
	id_obra INTEGER NOT NULL,
	PRIMARY KEY(id_evidencias)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE obra (
	id_obra INTEGER NOT NULL AUTO_INCREMENT,
	nombre_obra VARCHAR(50) NULL,
	no_beneficiarios INTEGER NOT NULL,
	id_persona VARCHAR(8) NOT NULL,
	PRIMARY KEY(id_obra)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE persona (
	id_persona VARCHAR(8) NOT NULL,
	nombre VARCHAR(50) NOT NULL,
	ap_pat VARCHAR(50) NOT NULL,
	ap_mat VARCHAR(50) NOT NULL,
	sexo VARCHAR(50) NOT NULL,
	contacto VARCHAR(50) NOT NULL,
	telefono1 VARCHAR(50) NOT NULL,
	telefono2 VARCHAR(13) NULL,
	foto MEDIUMBLOB NULL,
	activo VARCHAR(1) NULL,
	cp VARCHAR(10) NOT NULL,
	calle VARCHAR(50) NOT NULL,
	colonia VARCHAR(50) NOT NULL,
	municipio VARCHAR(50) NOT NULL,
	estado VARCHAR(50) NOT NULL,
	id_tipo_persona INTEGER(10) NOT NULL,
	PRIMARY KEY(id_persona)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE responsiva (
	id_responsiva INTEGER NOT NULL AUTO_INCREMENT,
	condiciones_equipo VARCHAR(500) NOT NULL,
	justificacion_salida VARCHAR(500) NOT NULL,
	fecha_salida TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	fecha_a_retorno DATETIME NOT NULL,
	fecha_retorno TIMESTAMP NOT NULL DEFAULT '0000-00-00 00:00:00',
	observ_retorno VARCHAR(500),
	id_equipo VARCHAR(8) NOT NULL,
	id_persona VARCHAR(8) NOT NULL,
	PRIMARY KEY(id_responsiva)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE tipo_persona (
	id_tipo_persona INTEGER NOT NULL AUTO_INCREMENT,
	tipo_persona VARCHAR(50) NOT NULL,
	PRIMARY KEY(id_tipo_persona)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE tipo_producto (
	id_tipo_producto INTEGER NOT NULL AUTO_INCREMENT,
	tipo_producto VARCHAR(50) NOT NULL,
	PRIMARY KEY(id_tipo_producto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE usuario (
	id_usuario INTEGER NOT NULL AUTO_INCREMENT,
	usuario VARCHAR(15) NOT NULL,
	contrasena VARCHAR(100) NOT NULL,
	id_persona VARCHAR(8) NOT NULL,
	PRIMARY KEY(id_usuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE vertiente (
	id_vertiente INTEGER NOT NULL AUTO_INCREMENT,
	nombre_vertiente VARCHAR(50) NOT NULL,
	PRIMARY KEY(id_vertiente)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

CREATE TABLE vertiente_persona (
	id_vertiente INTEGER NOT NULL,
	id_persona VARCHAR(8) NOT NULL,
	PRIMARY KEY(id_vertiente)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

ALTER TABLE area_persona ADD FOREIGN KEY (id_area) REFERENCES area(id_area);
ALTER TABLE area_persona ADD FOREIGN KEY (id_persona) REFERENCES persona(id_persona);

ALTER TABLE equipo ADD FOREIGN KEY (id_tipo_producto) REFERENCES tipo_producto(id_tipo_producto);

ALTER TABLE evidencias ADD FOREIGN KEY (id_obra) REFERENCES obra(id_obra);

ALTER TABLE obra ADD FOREIGN KEY (id_persona) REFERENCES persona(id_persona);

ALTER TABLE persona ADD FOREIGN KEY (id_tipo_persona) REFERENCES tipo_persona(id_tipo_persona);

ALTER TABLE responsiva ADD FOREIGN KEY (id_equipo) REFERENCES equipo(id_equipo);
ALTER TABLE responsiva ADD FOREIGN KEY (id_persona) REFERENCES persona(id_persona);

ALTER TABLE usuario ADD FOREIGN KEY (id_persona) REFERENCES persona(id_persona);

ALTER TABLE vertiente_persona ADD FOREIGN KEY (id_persona) REFERENCES persona(id_persona);
ALTER TABLE vertiente_persona ADD FOREIGN KEY (id_vertiente) REFERENCES vertiente(id_vertiente);

CREATE VIEW tbl_persona AS
	SELECT id_persona, nombre, ap_pat, ap_mat, sexo, contacto, telefono1, telefono2, foto, activo, cp, calle, colonia, municipio, estado, tipo_persona.id_tipo_persona, tipo_persona.tipo_persona
	FROM persona, tipo_persona
	WHERE persona.id_tipo_persona = tipo_persona.id_tipo_persona;

CREATE VIEW tbl_login AS
	SELECT id_usuario, usuario, contrasena, tbl_persona.id_persona, tbl_persona.nombre, tbl_persona.ap_pat, tbl_persona.ap_mat, tbl_persona.tipo_persona
	FROM usuario, tbl_persona
	WHERE usuario.id_persona = tbl_persona.id_persona;

CREATE VIEW tbl_equipo AS
	SELECT id_equipo, marca, serie, descripcion, tipo_producto.id_tipo_producto, tipo_producto.tipo_producto
	FROM equipo, tipo_producto
	WHERE equipo.id_tipo_producto = tipo_producto.id_tipo_producto;

CREATE VIEW tbl_responsiva AS
	SELECT id_responsiva, condiciones_equipo, justificacion_salida, fecha_salida, fecha_a_retorno, fecha_retorno, observ_retorno, tbl_equipo.id_equipo, tbl_equipo.marca, tbl_persona.id_persona, tbl_persona.nombre, tbl_persona.ap_pat, tbl_persona.ap_mat
	FROM responsiva, tbl_equipo, tbl_persona
	WHERE responsiva.id_equipo = tbl_equipo.id_equipo && responsiva.id_persona = tbl_persona.id_persona;

CREATE VIEW tbl_obra AS
	SELECT id_obra, nombre_obra, no_beneficiarios, tbl_persona.id_persona, nombre, ap_pat, ap_mat
	FROM obra, tbl_persona
	WHERE obra.id_persona = tbl_persona.id_persona;

CREATE VIEW tbl_evidencias AS
	SELECT id_evidencias, num_fotos, num_videos, num_cphs, actualizo, fecha, tbl_obra.id_obra, tbl_obra.nombre_obra
	FROM evidencias, tbl_obra
	WHERE evidencias.id_obra = tbl_obra.id_obra;

CREATE VIEW tbl_area_persona AS
	SELECT area_persona.id_area, area.nombre_area, area_persona.id_persona, tbl_persona.nombre, tbl_persona.ap_pat, tbl_persona.ap_mat
	FROM area_persona, area, tbl_persona
	WHERE area_persona.id_area = area.id_area && area_persona.id_persona = tbl_persona.id_persona;
	
CREATE VIEW tbl_vertiente_persona AS
	SELECT vertiente_persona.id_vertiente, vertiente.nombre_vertiente, vertiente_persona.id_persona, tbl_persona.nombre, tbl_persona.ap_pat, tbl_persona.ap_mat
	FROM vertiente_persona, vertiente, tbl_persona
	WHERE vertiente_persona.id_vertiente = vertiente.id_vertiente && vertiente_persona.id_persona = tbl_persona.id_persona;

/*Admin account */
INSERT INTO tipo_persona(id_tipo_persona, tipo_persona) VALUES (NULL, 'Administrador');
INSERT INTO tipo_persona(id_tipo_persona, tipo_persona) VALUES (NULL, 'Usuario'); 

INSERT INTO persona(id_persona, nombre, ap_pat, ap_mat, sexo, contacto, telefono1, telefono2, foto, activo, cp, calle, colonia, municipio, estado, id_tipo_persona)
VALUES ('mfn', 'Mauricio', 'Flores', 'Nicolas', 'Masculino', 'Telefono', '1122334455', NULL, NULL, 'S', '00000', 'Calle', 'Colonia', 'Municipio', 'Estado', '1'); 

INSERT INTO usuario(id_usuario, usuario, contrasena, id_persona) VALUES (NULL, 'admin', '21232f297a57a5a743894a0e4a801fc3', 'mfn');

