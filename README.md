# README: Solución Reto Sophos

<aside>
💡 Este documento describe la solución para el reto Sophos, el cual consiste en el desarrollo de una aplicación para la gestión de alquileres de una tienda de videojuegos.

Para obtener el repositorio ingresar a **[SOPHOS GAME LIBRARY REPO](https://github.com/jwalvarezuninorte/sophos-game-library-backend)**

**Tabla de contenido**

1. Paso a paso para ejecutar el proyecto
2. Diseño de base de datos, diagrama y modelo relacional (**BASE_DATOS**)
3. Scripts de creación y población de la base de datos
4. Prototipo - No aplica, proyecto backend
5. Documento técnico del servicio web expuesto 
6. Conclusiones
</aside>

## Pasos para ejecutar el proyecto

1. Clonar el repositorio: Abre una terminal y ejecuta el siguiente comando para clonar el repositorio del proyecto:
    
    ```bash
    git clone https://github.com/jwalvarezuninorte/sophos-game-library-backend
    ```
    
2. Instalar .NET 7: Asegúrate de tener instalado .NET 7 en tu máquina. Puedes descargarlo desde el sitio oficial de .NET.
3. Obtener la imagen de SQL Server: Descarga la imagen de SQL Server desde el registro de contenedores de Docker. Ejecuta el siguiente comando en la terminal:
    
    ```bash
    docker pull mcr.microsoft.com/azure-sql-edge
    ```
    
4. Configurar la cadena de conexión: En el proyecto, busca el archivo de configuración **`appsettings.json`** y modifica la cadena de conexión para que apunte a tu instancia de SQL Server. Asegúrate de proporcionar el nombre de servidor, el puerto, el nombre de la base de datos, el usuario y la contraseña correspondientes.
5. Población de datos en SQL Server: Ejecuta los scripts SQL proporcionados en el repositorio para crear las tablas necesarias y poblar la base de datos con datos de ejemplo. Puedes ejecutar los scripts utilizando una herramienta de administración de bases de datos, como SQL Server Management Studio.
6. Ejecutar el proyecto: Abre una terminal, navega hasta la carpeta raíz del proyecto y ejecuta el siguiente comando:
    
    ```bash
    dotnet run
    ```
    
    Esto compilará y ejecutará el proyecto.
    
7. Acceder al servicio web: Una vez que el proyecto se esté ejecutando, puedes acceder al servicio web utilizando un navegador o una herramienta como Postman. Consulta la documentación del proyecto para obtener detalles sobre las rutas y los parámetros disponibles.
    
    ![Screenshot 2023-05-26 at 1.52.44 AM.png](README%20Solucio%CC%81n%20Reto%20Sophos%20e426d1d54fb549d3a10bde2cde6394ea/Screenshot_2023-05-26_at_1.52.44_AM.png)
    
    ![Screenshot 2023-05-26 at 1.53.36 AM.png](README%20Solucio%CC%81n%20Reto%20Sophos%20e426d1d54fb549d3a10bde2cde6394ea/Screenshot_2023-05-26_at_1.53.36_AM.png)
    

## Diseño de base de datos, diagrama y modelo relacional (**BASE_DATOS**)

![db_sophos_game_library.png](README%20Solucio%CC%81n%20Reto%20Sophos%20e426d1d54fb549d3a10bde2cde6394ea/db_sophos_game_library.png)

## Scripts de creación y población de la base de datos

```sql
create database db_sophos_game_library collate SQL_Latin1_General_CP1_CI_AS
go

create table dbo.Games
(
    id                  int          not null
        constraint PK_Game
            primary key,
    name                varchar(50)  not null,
    description         varchar(255) not null,
    rental_price        float        not null,
    selling_price       float,
    director_name       varchar(50),
    productor_name      varchar(50),
    launch_date         date         not null,
    lead_character_name varchar(50),
    game_platform       varchar(50)  not null
)
go

create table dbo.Users
(
    id       int identity
        constraint PK_User
            primary key,
    name     varchar(50) not null,
    email    varchar(50) not null,
    phone    varchar(50),
    address  varchar(50) not null,
    birthday date,
    role     varchar(50) not null
)
go

create table dbo.Rentals
(
    id             int           not null
        constraint PK_Rental
            primary key,
    fk_rental_user int           not null
        constraint fk_rental_user
            references dbo.Users,
    fk_rental_game int           not null
        constraint fk_rental_game
            references dbo.Games,
    date_start     smalldatetime not null,
    date_end       smalldatetime not null
)

```

```bash
INSERT INTO db_sophos_game_library.dbo.Games (id, name, description, rental_price, selling_price, director_name, productor_name, launch_date, lead_character_name, game_platform)
VALUES
    (1, N'GTA III', N'Juego de GTA III', 20, 39, N'Rockstar', N'Rockstar', N'2023-05-17', N'CJ', N'PC'),
    (2, N'GTA IV', N'Juego de GTA IV', 20, 39, N'Rockstar', N'Rockstar', N'2023-05-17', N'CJ', N'PC'),
    (3, N'FIFA 23', N'Juego de FIFA 23', 25, 49, N'EA Sports', N'EA', N'2023-09-28', N'Lionel Messi', N'PlayStation'),
    (4, N'Call of Duty: Warzone', N'Juego de Call of Duty: Warzone', 0, 59, N'Activision', N'Activision', N'2020-03-10', N'Captain Price', N'Xbox'),
    (5, N'The Legend of Zelda: Breath of the Wild', N'Juego de The Legend of Zelda: Breath of the Wild', 15, 59, N'Eiji Aonuma', N'Nintendo', N'2017-03-03', N'Link', N'Nintendo Switch');

INSERT INTO db_sophos_game_library.dbo.Users (id, name, email, phone, address, birthday, role)
VALUES
    (1, N'Juan Lopez', N'juanlopez@gmail.com', N'123123123', N'cll110j #17l', N'1998-10-01', N'customer'),
    (2, N'Maria Garcia', N'mariagarcia@gmail.com', N'456456456', N'av123 #45', N'1995-05-15', N'customer'),
    (3, N'Carlos Ramirez', N'carlosramirez@gmail.com', N'789789789', N'cll456 #78', N'2000-12-30', N'customer'),
    (4, N'Sofia Martinez', N'sofiamartinez@gmail.com', N'987987987', N'av9876 #32', N'1993-08-25', N'customer'),
    (5, N'Pedro Gonzalez', N'pedrogonzalez@gmail.com', N'654654654', N'cll7890 #91', N'1997-03-05', N'customer');

INSERT INTO db_sophos_game_library.dbo.Rentals (id, fk_rental_user, fk_rental_game, date_start, date_end)
VALUES
    (1, 1, 2, N'2023-05-26 02:34:14', N'2023-05-26 02:34:16'),
    (2, 3, 4, N'2023-05-27 08:15:21', N'2023-05-27 09:45:32'),
    (3, 2, 1, N'2023-05-28 14:22:45', N'2023-05-28 16:57:59'),
    (4, 4, 3, N'2023-05-29 19:10:37', N'2023-05-29 20:25:42'),
    (5, 5, 2, N'2023-05-30 22:08:19', N'2023-05-30 23:59:59');
```

Este proyecto consiste en gestionar todo el proceso básico de alquiler de una tienda de videojuegos. El objetivo principal es almacenar la información básica de los clientes, así como la información de los juegos disponibles para alquiler. A continuación se detallan los requisitos y funcionalidades solicitadas por el dueño de la tienda:

---

1. Almacenamiento de información de clientes: Se debe almacenar la información básica de los clientes, lo cual permitirá conocer quién tiene alquilado un juego y facilitará al dueño reclamarlo cuando se venza el periodo de alquiler.
2. Definición de precios de alquiler: El sistema debe permitir al dueño de la tienda definir el precio de los alquileres, los cuales pueden cambiar periódicamente según el título del juego.
3. Cliente más frecuente: El sistema debe proporcionar la funcionalidad de conocer quién es el cliente más frecuente, es decir, aquel que ha realizado más alquileres en la tienda.
4. Título de juego más rentado: El sistema debe permitir identificar cuál es el título de juego más rentado, es decir, el juego que ha sido alquilado con mayor frecuencia.
5. Registro de alquileres y generación de prueba de compra: Se debe permitir registrar todos los alquileres realizados en la tienda y generar una prueba de compra para cada alquiler.
6. Consultas de juegos: El sistema debe proporcionar la funcionalidad de consultar listados de juegos según diferentes criterios, como director de juego, protagonistas del juego, productor o marca del juego, y fecha de lanzamiento.
7. Juego menos rentado por rangos de edad: El sistema debe permitir identificar cuál es el juego menos rentado en rangos de edad de los clientes, agrupados en intervalos de 10 años.
8. Registro de información de cada juego: Se debe tener registrado, para cada juego, el nombre, año de lanzamiento, protagonistas, director, productor y plataforma en la que está disponible (Xbox, PlayStation, Nintendo, PC).
9. Servicio web para consultar información: El dueño desea exponer un servicio web que permita a cualquier cliente consultar su balance, fecha de entrega y títulos alquilados.

## **Tecnologías Utilizadas**

Para la implementación de este proyecto, se utilizó el lenguaje de programación **.NET** y el sistema de gestión de bases de datos **SQL Server.** Estas tecnologías ofrecen un conjunto de herramientas robustas y eficientes para desarrollar y gestionar la aplicación de alquiler de videojuegos.

## Endpoints

| Ruta | Método | Descripción | Parámetros | Cuerpo de solicitud | Respuestas |
| --- | --- | --- | --- | --- | --- |
| /api/Game | GET | Obtener juegos | - | - | 200: Array<GameDto> |
| /api/Game | POST | Crear juego | - | GameDto | 200: GameDto |
| /api/Game/{id} | GET | Obtener juego | id (integer) | - | 200: GameDto |
| /api/Game/{id} | PUT | Actualizar juego | id (integer) | GameDto | 200: Success |
| /api/Game/{id} | DELETE | Eliminar juego | id (integer) | - | 200: Success |
| /api/Rentals | GET | Obtener alquileres | - | - | 200: Array<RentalDto> |
| /api/Rentals | POST | Crear alquiler | - | Rental | 200: Rental |
| /api/Rentals/{id} | GET | Obtener alquiler | id (integer) | - | 200: RentalDto |
| /api/Rentals/{id} | PUT | Actualizar alquiler | id (integer) | RentalDto | 200: Success |
| /api/Rentals/{id} | DELETE | Eliminar alquiler | id (integer) | - | 200: Success |
| /api/Rentals/most-rented-games | GET | Obtener juegos más alquilados | - | - | 200: Array<GameDto> |
| /api/User | GET | Obtener usuarios | - | - | 200: Array<UserDto> |
| /api/User | POST | Crear usuario | - | UserDto | 200: User |
| /api/User/{id} | GET | Obtener usuario | id (integer) | - | 200: UserDto |
| /api/User/{id} | PUT | Actualizar usuario | id (integer) | UserDto | 200: Success |
| /api/User/{id} | DELETE | Eliminar usuario | id (integer) | - | 200: Success |

## **Conclusiones**

El desarrollo de este proyecto de gestión de alquiler de una tienda de videojuegos con .NET y SQL Server permite satisfacer las necesidades del dueño de la tienda. Con las funcionalidades mencionadas, se garantiza un control efectivo sobre los alquileres realizados, la información de los clientes y los juegos disponibles. Además, la exposición de un servicio web brinda comodidad y accesibilidad a los clientes para consultar su información personal.

La combinación de .NET y SQL Server ofrece una solución escalable y segura, permitiendo el almacenamiento eficiente de los datos y un rendimiento óptimo en el manejo de las consultas y operaciones del sistema.