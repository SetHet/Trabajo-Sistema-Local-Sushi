DELETE FROM producto_pedido;
DELETE FROM pedido;
DELETE FROM cliente;

INSERT INTO cliente VALUES('123.456.789-N', 'BRYAN', 'PINO', 456456456, 'B.PINO@GMAIL.COM');
INSERT INTO pedido VALUES(0, 'DIR', 456456, 15000, 'sin sal todo', sysdate, 0,  '123.456.789-N');
INSERT INTO producto_pedido VALUES(0, 2, 'ZERO', 0, 0);
INSERT INTO producto_pedido VALUES(1, 5, 'CON MUCHA SAL', 0, 1);

select seq_nroPedido.nextval,seq_nroPedido.currval from dual;

UPDATE pedido SET id_estado = 0;

select c.rut, c.nombre || ' ' || c.apellido, p.nro_pedido
                    from cliente c join pedido p on(c.rut = p.rut_cliente)
                    where p.id_estado = 0;
                  
select * from pedido;
select * from producto_pedido;