--aqui se insertan los productos,  estado pedido, tipo de producto
DELETE FROM producto;
DELETE FROM tipo_producto;
DELETE FROM estado_pedido;

INSERT INTO estado_pedido VALUES(0, 'PENDIENTE');
INSERT INTO estado_pedido VALUES(1, 'COMPLETADO');
select * from estado_pedido;

INSERT INTO tipo_producto VALUES(0, 'Bebida');
INSERT INTO tipo_producto VALUES(1, 'Salsa');
INSERT INTO tipo_producto VALUES(2, 'rolls');
INSERT INTO tipo_producto VALUES(3,'sushi');
select * from tipo_producto;

INSERT INTO producto VALUES(0, 'Coca cola 1 Lt.', 1000, 'Alto en azucares', 0);
INSERT INTO producto VALUES(1, 'Coca cola 2.5 Lt.', 1500, 'Alto en azucares', 0);
INSERT INTO producto VALUES(2, 'Fanta 1 Lt.', 1000, 'Alto en azucares', 0);
INSERT INTO producto VALUES(3, 'Fanta 2.5 Lt.', 1500, 'Alto en azucares', 0);
INSERT INTO producto VALUES(4, 'Sprite 1 Lt.', 1000, 'Alto en azucares', 0);
INSERT INTO producto VALUES(5, 'Sprite 2.5 Lt.', 1500, 'Alto en azucares', 0);
INSERT INTO producto VALUES(6, 'Agua sin gas 500cc', 600, 'sin sello', 0);
INSERT INTO producto VALUES(7, 'Agua Mineral 500cc', 600, 'sin sello', 0);
INSERT INTO producto VALUES(8, 'Energetica 500cc', 2000, 'Alto en azucares', 0);
INSERT INTO producto VALUES(9, 'Té', 500, 'Alto en azucares', 0);
INSERT INTO producto VALUES(10, 'Cafe', 500, 'Alto en azucares', 0);

INSERT INTO producto VALUES(11, 'Salsa szechuan', 500, 'Alto en sodio', 1);
INSERT INTO producto VALUES(12, 'Salsa acida', 500, 'Alto en sodio', 1);
INSERT INTO producto VALUES(13, 'Salsa de soya', 500, 'Alto en sodio', 1);
INSERT INTO producto VALUES(14, 'Salsa teriyaki', 500, 'Alto en sodio', 1);
INSERT INTO producto VALUES(15, 'Salsa agridulce', 600, 'Alto en sodio', 1);

INSERT INTO producto VALUES(16, 'Roll de pollo', 1000, 'Alto en sodio', 2);
INSERT INTO producto VALUES(17, 'Roll de kanikama', 2500, 'Alto en sodio', 2);
INSERT INTO producto VALUES(18, 'Roll de queso', 2500, 'Alto en sodio', 2);
INSERT INTO producto VALUES(19, 'Roll de camarón', 2500, 'Alto en sodio', 2);
INSERT INTO producto VALUES(20, 'Roll agridulce', 3000, 'Alto en sodio', 2);
INSERT INTO producto VALUES(21, 'Roll picante', 3000, 'Alto en sodio', 2);

INSERT INTO producto VALUES(22, 'Sushi de camarón', 3000, 'Alto en sodio', 3);
INSERT INTO producto VALUES(23, 'Sushi de  palta', 3000, 'Alto en sodio', 3);
INSERT INTO producto VALUES(24, 'Sushi vegano', 3000, 'Alto en sodio', 3);
INSERT INTO producto VALUES(25, 'Sushi de queso', 3000, 'Alto en sodio', 3);
INSERT INTO producto VALUES(26, 'Sushi de pollo', 3500, 'Alto en sodio', 3);
INSERT INTO producto VALUES(27, 'Sushi de atún', 3500, 'Alto en sodio', 3);
INSERT INTO producto VALUES(28, 'Sushi de kanikama', 4000, 'Alto en sodio', 3);
INSERT INTO producto VALUES(29, 'Sushi de cangrejo', 4000, 'Alto en sodio', 3);

select * from producto;

Commit;
