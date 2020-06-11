drop table producto_pedido;
drop table pedido;
drop table estado_pedido;
drop table cliente;
drop table producto;
drop table tipo_producto;

drop sequence seq_nroPedido;
drop sequence seq_idProductoPedido;

create sequence seq_nroPedido start with 0 minvalue 0;
create sequence seq_idProductoPedido start with 0 minvalue 0;

create table tipo_producto(
  id_tipo_producto Number(2) not null,
  nombre_tipo VARCHAR2(30) not null,
  CONSTRAINT pk_tipo_producto PRIMARY KEY (id_tipo_producto)
);

create table producto(
  id_producto NUMBER(3) NOT NULL PRIMARY KEY,
  nombre_producto VARCHAR2(45) NOT NULL,
  costo NUMBER(6) NOT NULL,
  descripcion VARCHAR(200),
  id_tipo NUMBER(2) NOT NULL,
  CONSTRAINT fk_tipo_producto FOREIGN KEY (id_tipo) REFERENCES tipo_producto(id_tipo_producto)
);

create table cliente(
  rut VARCHAR(15) NOT NULL PRIMARY KEY, -- 123.456.789-N
  nombre VARCHAR(50) NULL,
  apellido VARCHAR(50) NULL,
  telefono NUMERIC(25) NOT NULL,
  mail VARCHAR(100) NULL
);

CREATE TABLE estado_pedido(
  id_pedido NUMBER(2) NOT NULL PRIMARY KEY,
  nombre VARCHAR(25) NOT NULL
);

CREATE TABLE pedido(
  nro_pedido INTEGER NOT NULL PRIMARY KEY,
  direccion_envio VARCHAR(200) NULL,
  telefono NUMBER(25) NOT NULL,
  total_pago NUMBER(9) NOT NULL,
  detalle VARCHAR2(500) NULL,
  fecha_hora DATE NOT NULL,
  id_estado NUMBER(2) NOT NULL,
  rut_cliente VARCHAR(15) NOT NULL,
  CONSTRAINT fk_id_estado FOREIGN KEY (id_estado) REFERENCES estado_pedido(id_pedido),
  CONSTRAINT fk_rut_cliente FOREIGN KEY (rut_cliente) REFERENCES cliente(rut)
);

CREATE TABLE producto_pedido(
  id_producto_pedido INTEGER NOT NULL PRIMARY KEY,
  cantidad NUMBER(4) NOT NULL,
  descripcion VARCHAR2(100) NULL,
  nro_pedido INTEGER NOT NULL,
  id_producto NUMBER(3) NOT NULL,
  CONSTRAINT fk_nro_pedido FOREIGN KEY (nro_pedido) REFERENCES pedido(nro_pedido),
  CONSTRAINT fk_id_producto FOREIGN KEY (id_producto) REFERENCES producto(id_producto)
);

