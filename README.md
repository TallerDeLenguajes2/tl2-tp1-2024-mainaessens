# Punto 2.a

* La relación entre el pedido y el cliente es una de composición, ya que al eliminar el pedido, también se elimina el cliente, pues no es necesario que la información del cliente persista por separado. En cambio, entre el cadete y el pedido existe una relación débil de agregación, ya que la existencia del pedido no depende del cadete. Los pedidos se asignan a un cadete, pero si este deja de existir, el pedido se mantiene y se reasigna a otro cadete. Finalmente, la relación entre los cadetes y la cadetería también es débil. Los cadetes se crean y luego son registrados en la cadetería.

* La cadetería incluye el método AsignarPedidos, que asigna un pedido al cadete con menos pedidos al recibirlo. También cuenta con el método ReasignarPedidos, que transfiere un pedido específico de un cadete a otro. Otro método es MostrarJornalesYEnvios, el cual muestra los jornales de cada cadete y los envíos realizados por ellos, además del total de envíos y el promedio de envíos por cadete. 

El cadete tiene los métodos CantidadDePedidosCompletados y JornalACobrar. El primero utiliza una consulta Linq para obtener, a partir de la lista de pedidos, la cantidad de pedidos completados. El segundo calcula el jornal correspondiente al cadete en función de los pedidos completados. Además, el método RetirarPedido cambia el estado del pedido de "Preparación" a "En camino", mientras que EntregarPedido actualiza el estado a "Entregado". Por último, el método DarDeBajaPedido elimina un pedido específico de la lista de pedidos del cadete.*

* Todos los atributos de las clases son privados, y su accesibilidad se maneja a través de las propiedades *get* y *set*. Para campos cuyos valores no cambian durante el uso del sistema, como nombre, teléfono, dirección, etc., se utiliza únicamente la propiedad *get* para mostrar su información en pantalla. En cambio, los campos como la lista de pedidos o el estado de los pedidos, que pueden cambiar durante el uso del sistema, cuentan con la propiedad *set* para reflejar los cambios realizados por el usuario. El acceso y la manipulación de los campos se llevan a cabo mediante las propiedades y métodos de cada clase, que se mantienen públicos.

* El constructor de la clase Pedido recibe la información necesaria (nombre y observaciones) y establece el estado inicial como "En preparación". También recibe la información del cliente (nombre, dirección, teléfono, referencias), y dado que existe una relación de composición, dentro del constructor de Pedido se invoca al constructor de la clase Cliente para crear el objeto Cliente que formará parte del pedido.

El constructor de la clase Cadete recibe los datos del cadete (nombre, dirección, teléfono) y inicializa una lista de pedidos vacía.

Finalmente, la clase Cadetería se construye con la información específica de la cadetería (nombre y teléfono) junto con una lista de cadetes, que se crea previamente, ya que se trata de una relación de agregación.

* Consideraría que la relación entre los clientes y los pedidos debería ser de agregación en lugar de composición, ya que esto permitiría que la cadetería realice un análisis de los clientes de forma independiente del pedido.