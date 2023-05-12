Nuestra web consiste en una tienda de videojuegos en la que los usuarios podrán ver todos los videojuegos y productos relacionados. Para poder comprar productos tendrán que loguearse. Una vez logueados podrán comprar cualquier producto con stock disponible de la web y podrán acceder a su área personal donde ver sus productos comprados.

Nuestro modelo de datos consta de 3 entidades: usuarios, productos y la tabla intermedia de productos de usuarios, ya que el mismo producto puede ser comprado por varios usuarios y un usuario puede comprar varios productos. A continuación detallamos los atributos de cada entidad:

Usuarios: id, usuario, contraseña, rol y fecha de registro Productos: id, nombre, descripción, precio y cantidad disponible Productos por usuario: id compra, id de usuario, id de producto

Todo el mundo que acceda a la web, aunque no tenga un usuario asignado, podrá ver el listado de productos completo. Los usuarios podrán registrarse y loguearse para comprar productos y acceder a su área personal donde verán su info de usuario y sus compras. Los usuarios visitantes no tendrán un rol asignado. Los administradores de la web sí tendrán un rol asignado, lo que les permitirá añadir, editar y eliminar productos, acción restringida a este rol.

Credenciales para loguearse y utilizar la aplicación (estos usuarios están ya precargados en bbdd):
Usuario administrador: username:admin password:admin
Usuario visitante: username:edu password:edu12345