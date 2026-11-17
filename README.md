# ServiTec Manager

Sistema web de gestión para un taller de reparación. Enlaza cada equipo recibido con su diagnóstico, cotización e inventario mediante un ticket único.

Proyecto de la asignatura Programación III (ITLA), Tarea 3: Uso de Git y Git Flow. Grupo 3.

## Alcance de esta versión

**Incluye:**

- Usuarios y roles internos: Administrador, Recepción y Técnico.
- Clientes y fichas de equipos.
- Tickets de ingreso con folio único y estados.
- Diagnósticos, asignación de técnico e historial de transiciones.
- Repuestos e inventario: entradas, salidas simples, devoluciones y alerta de stock mínimo.
- Cotizaciones versionadas (módulo opcional).

**No incluye:** portal externo del cliente, reserva y consumo de repuestos ligados al ticket, comprobantes y entrega, historial navegable del equipo, notificaciones automáticas, reportes avanzados, garantías, aplicación móvil, varias sucursales, facturación fiscal y pagos electrónicos.

## Stack tecnológico

| Capa | Tecnología |
|---|---|
| API | ASP.NET Core Web API (.NET 10) con Clean Architecture |
| Base de datos | PostgreSQL con Entity Framework Core (Npgsql) |
| Seguridad | JWT y BCrypt |
| Interfaz | Next.js (App Router), TypeScript y Tailwind CSS |

## Estructura del repositorio

```text
servitec-manager/
├── frontend/                    # Interfaz de usuario (Next.js)
│   └── src/
│       ├── app/                 # Enrutamiento
│       ├── components/          # Componentes, una carpeta por módulo
│       ├── lib/api/             # Llamadas a la API, un archivo por módulo
│       └── types/               # Interfaces TypeScript
├── backend/
│   ├── ServiTec.slnx
│   ├── ServiTec.Domain/         # Entidades, enums y excepciones
│   ├── ServiTec.Application/    # DTOs, interfaces y servicios
│   ├── ServiTec.Infrastructure/ # EF Core, repositorios y seguridad
│   └── ServiTec.Backend/        # API: controllers, middlewares y Program.cs
├── .github/pull_request_template.md
├── .env.example                 # Plantilla de variables de entorno
├── CONTRIBUTING.md              # Git Flow y convenciones del equipo
└── README.md
```

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) 20.9 o superior
- [PostgreSQL](https://www.postgresql.org/download/) 16 o superior. Todo el equipo debe usar la misma versión mayor.
- Git

## Puesta en marcha

### 1. Clonar el repositorio

Clona el proyecto en una carpeta que **no** esté sincronizada con OneDrive.

```powershell
https://github.com/Sebastian-Ferreras/servitec-manager.git
cd servitec-manager
```

### 2. Base de datos

Instala PostgreSQL y recuerda la contraseña del usuario `postgres`. No hace falta crear la base de datos a mano: al arrancar en Development, la API crea `servitec_manager`, aplica las migraciones y siembra los usuarios de prueba. Solo necesita que PostgreSQL esté corriendo.

### 3. Variables de entorno

Copia las plantillas:

```powershell
Copy-Item .env.example .env
Copy-Item frontend\.env.example frontend\.env.local
```

Edita `.env` en la raíz:

| Variable | Qué poner |
|---|---|
| `ConnectionStrings__DefaultConnection` | Cadena de conexión a tu PostgreSQL; cambia `CAMBIAR` por tu contraseña. |
| `Jwt__Key` | Cualquier cadena de al menos 32 caracteres. |
| `Seed__DefaultPassword` | Contraseña inicial de los usuarios de prueba. |

Los archivos `.env` y `.env.local` están ignorados por Git y no se suben nunca.

### 4. Ejecutar el backend

```powershell
dotnet run --project backend/ServiTec.Backend
```

La API queda en `http://localhost:5067`. Para comprobarla: `GET http://localhost:5067/api/health`. En Development la definición OpenAPI está en `/openapi/v1.json`.

### 5. Ejecutar el frontend

```powershell
cd frontend
npm install
npm run dev
```

La interfaz queda en `http://localhost:3000`.

## Usuarios de prueba

El seed crea tres usuarios, uno por rol. La contraseña es el valor de `Seed__DefaultPassword`.

| Rol | Correo |
|---|---|
| Administrador | `admin@servitec.local` |
| Recepción | `recepcion@servitec.local` |
| Técnico | `tecnico@servitec.local` |

## Decisión de seguridad

Las contraseñas se guardan con **BCrypt** (hash adaptativo con sal). Nunca se almacenan en texto plano ni aparecen en respuestas o logs.

## Cómo contribuir

Antes de crear una rama o abrir un PR, lee [CONTRIBUTING.md](CONTRIBUTING.md): ahí están el Git Flow, las convenciones de commits y las reglas de revisión.
