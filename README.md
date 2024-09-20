
# ApiRestful

Este proyecto es una API RESTful construida con ASP.NET Core y Entity Framework Core. Proporciona endpoints para gestionar categorías, productos, usuarios y elementos de la lista de deseos.

## Requisitos

- .NET 8 SDK o superior
- SQL Server

## Configuración

1. Clona el repositorio:
   ```bash
   git clone https://github.com/yayomanosalva/ApiRestful.git
   cd ApiRestful
   ```

2. Configura la cadena de conexión a tu base de datos en `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "ConexionSql": "Data Source=(local)\\SQLEXPRESS;Server=localhost;Database=BdProductos;Trusted_Connection=True;TrustServerCertificate=true"
     }
   }
   ```

3. Aplica las migraciones para crear la base de datos:
   ```bash
   dotnet ef database update
   ```
   o en su defecto
   ```bash
   Add-Migration "Primera Migracion"
   ```

## Ejecución

Para ejecutar la aplicación, usa el siguiente comando:
```bash
dotnet run
```
o en su defecto
   ```bash
   Update-Database
   ```

La API estará disponible en `https://localhost:5001` o `http://localhost:5000`.

## Endpoints

### Categorías

- **GET** `/api/categories` - Obtiene todas las categorías.
- **GET** `/api/categories/{id}` - Obtiene una categoría por ID.
- **POST** `/api/categories` - Crea una nueva categoría.
- **PUT** `/api/categories/{id}` - Actualiza una categoría existente.
- **DELETE** `/api/categories/{id}` - Elimina una categoría.

### Productos

- **GET** `/api/products` - Obtiene todos los productos.
- **GET** `/api/products/{id}` - Obtiene un producto por ID.
- **POST** `/api/products` - Crea un nuevo producto.
- **PUT** `/api/products/{id}` - Actualiza un producto existente.
- **DELETE** `/api/products/{id}` - Elimina un producto.

### Usuarios

- **GET** `/api/users` - Obtiene todos los usuarios.
- **GET** `/api/users/{id}` - Obtiene un usuario por ID.
- **POST** `/api/users` - Crea un nuevo usuario.
- **PUT** `/api/users/{id}` - Actualiza un usuario existente.
- **DELETE** `/api/users/{id}` - Elimina un usuario.

### Wishlist Items

- **GET** `/api/wishlistitems` - Obtiene todos los elementos de la lista de deseos.
- **GET** `/api/wishlistitems/{id}` - Obtiene un elemento de la lista de deseos por ID.
- **POST** `/api/wishlistitems` - Crea un nuevo elemento en la lista de deseos.
- **PUT** `/api/wishlistitems/{id}` - Actualiza un elemento existente en la lista de deseos.
- **DELETE** `/api/wishlistitems/{id}` - Elimina un elemento de la lista de deseos.

## Estructura del Proyecto

- **Controllers**: Contiene los controladores de la API.
  - `CategorysController.cs`
  - `ProductController.cs`
  - `UserController.cs`
  - `WishlistItemController.cs`

- **DTOs**: Contiene los Data Transfer Objects.
  - `CategoryDTO.cs`
  - `ProductDTO.cs`
  - `UserDTO.cs`
  - `WishlistItemDTO.cs`

- **Entities**: Contiene las entidades del modelo de datos.
  - `Category.cs`
  - `Product.cs`
  - `User.cs`
  - `WishlistItem.cs`

## Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue o envía un pull request.

## Licencia

Este proyecto está licenciado bajo los términos de la licencia MIT. Consulta el archivo `LICENSE` para más detalles.
