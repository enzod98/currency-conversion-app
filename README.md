ASP.NET CORE 10.0 - MINIMAL APIS

API para la gestión de usuarios y conversiones de divisas, desarrollada utilizando patrones de diesño y buenas prácticas


* **Runtime:** [.NET 10](https://dotnet.microsoft.com/download/dotnet/10.0)
* **API Framework:** ASP.NET Core Minimal APIs
* **ORM:** Entity Framework Core (SQLite)
* **Mediator Pattern:** [MediatR](https://github.com/jbogard/MediatR) (CQRS)
* **Validaciones:** [FluentValidation](https://fluentvalidation.net/)
* **Documentación:** Swagger / OpenAPI

---

### Instalaciones Previas

* [.NET SDK 10.0](https://dotnet.microsoft.com/download/dotnet/10.0)

### Instalar y ejecutar la API

1.  **Clonar el repositorio**
    ```bash
    git clone [https://github.com/enzod98/currency-conversion-app.git](https://github.com/enzod98/currency-conversion-app.git)
    ```

2.  **Ejecutar la API**
    Navega a la carpeta del proyecto principal y ejecuta:
    ```bash
    cd Currency.API
    dotnet run
    ```
    > **Nota:** Al iniciar, la aplicación detectará si la base de datos existe. Si no, **ejecutará las migraciones automáticamente** y creará el archivo `.db`.

### Migraciones Manuales (Opcional)

Si prefieres gestionar la base de datos manualmente:
```bash
cd Infrastructure
dotnet ef database update
```

---

Seguridad y Autenticación

Todas las rutas de la API están protegidas. Para realizar peticiones con éxito, se debe incluir una **API Key** en la cabecera (header) de cada solicitud.

| Header Key | Valor por Defecto | Estado |
| :--- | :--- | :--- |
| `X-API-KEY` | `Prueba_Tecnica_Enzo_Dure` | Obligatorio |

> **Nota:** Si la clave no es enviada o es incorrecta, la API responderá con un código `401 Unauthorized`.

Características implementadas
- **Arquitectura Limpia:** Separación de responsabilidades (Domain, Application, Infrastructure, API).
- **CQRS:** Implementado con [MediatR](https://github.com/jbogard/MediatR).
- **Validación Robusta:** Reglas de negocio validadas con [FluentValidation](https://fluentvalidation.net/).
- **Base de Datos:** Persistencia ligera con **SQLite** y Entity Framework Core.
- **Gestión de Usuarios (CRUD):** Endpoints para la administración de Usuarios.
- **Direcciones Relacionadas a Usuarios (1:N):** Endpoints para la administración de direcciones con una relación **1:N** vinculada a los usuarios.
- **Módulo de Conversión de Divisas:** Endpoints para la administración de Tipos de divisa y cálculos de conversiones.
- **Seguridad:** - Autenticación mediante **API Key** (Middleware personalizado).
  - Hashing de contraseñas nativo (SHA256).
- **Documentación:** API documentada automáticamente con **Swagger**.

---

## Documentación Interactiva (Swagger)

La API incluye una interfaz de Swagger para facilitar las pruebas de los endpoints.

1. Inicie la aplicación.
2. Navegue a: `https://localhost:XXXX/swagger` (el puerto se indica en la consola).
3. Haga clic en el botón **Authorize**.
4. Ingrese el valor de la API Key por defecto: `Prueba_Tecnica_Enzo_Dure`.

---


<p align="center">
  <b>Desarrollado por Enzo Dure</b><br>
  <i>Ing.Informático</i>
</p>
