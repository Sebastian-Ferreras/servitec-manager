# Guía de contribución

Esta guía define cómo se trabaja en ServiTec Manager: ramas, commits, Pull Requests y convenciones de código. Para levantar el proyecto en local, consulta el [README](README.md).

## Git Flow

El trabajo entra a `develop` por Pull Request, se verifica en `qa` y llega a `main` solo cuando está validado.

### Ramas permanentes

| Rama | Propósito | Regla |
|---|---|---|
| `main` | Código estable y entregable | Solo recibe cambios por PR; la única excepción es el commit inicial del esqueleto. |
| `qa` | Verificación contra los criterios de aceptación | Solo por PR. |
| `develop` | Integración de todas las features | Solo por PR. |

Nadie hace commits directos en estas tres ramas.

### Ramas temporales

- `feature/<modulo>`: una por módulo (por ejemplo `feature/tickets-ingreso`).
- `hotfix/<descripcion>`: corrección urgente de código que ya está en `main`.

Los nombres van en minúsculas, con guiones y **sin anidar** (`feature/tickets-ingreso`, no `feature/tickets/ingreso`: Git no permite una rama y una subrama con el mismo prefijo).

### Ciclo de una feature

```bash
git checkout dev
git pull
git checkout -b feature/tickets-ingreso

# commits pequeños y frecuentes
git add <archivos>
git commit -m "feat: agregar generación de folio único para tickets"
git push -u origin feature/tickets-ingreso

# PR de backend hacia develop, revisión y merge
git fetch origin
git merge origin/develop          # antes de empezar el frontend
# PR de frontend hacia develop, revisión y merge
```

### Flujo de Pull Requests

Los pasos se hacen en este orden para cada rama:

| Paso | Pull Request |
|---|---|
| 1 | `feature/*` → `develop` (backend) |
| 2 | `feature/*` → `develop` (frontend) |
| 3 | `develop` → `qa`, una feature a la vez, cuando la rama pasó la revisión de sus criterios de aceptación |
| 4 | `qa` → `main` |

El **hotfix** nace de `main`: su PR va a `main` y luego se devuelve a `develop` y a `qa` para que no se pierda la corrección.

### Reglas de merge y revisión

- Nadie mergea su propio PR sin la aprobación de otra persona. Los PRs hacia `qa` y `main`.
- Método de merge: **Create a merge commit**. No se hace squash ni rebase de ramas compartidas.
- Un PR no se mergea con conflictos ni con la compilación rota.
- Todos los PRs se cierran el mismo día que se abren.
- **Las ramas no se borran al mergear.**

### Sincronización diaria

Cada persona ejecuta lo siguiente en su rama al empezar el día y antes de abrir un PR:

```bash
git fetch origin
git merge origin/dev
```

`fetch` solo descarga los cambios; `merge` los integra en tu rama.

#### Si hay conflictos

1. Git marca los archivos en conflicto; ábrelos y busca los marcadores `<<<<<<<`, `=======` y `>>>>>>>`.
2. Deja el código correcto (conservando lo de ambos lados cuando aplique) y borra los marcadores.
3. `git add <archivo>` y luego `git commit` para cerrar el merge.
4. Compila y levanta el proyecto antes de hacer push.
5. Si el conflicto es en un archivo de otro módulo, consulta con quien lo mantiene.

### Archivos compartidos

Cada módulo trabaja en archivos propios de cada capa (por ejemplo `TicketService.cs`, `ITicketService.cs`, `TicketRepository.cs`, `TicketController.cs`, `src/components/tickets/`, `src/lib/api/tickets.ts` y `src/types/ticket.ts`).

Los archivos compartidos (menú, layout, cliente de API, `Program.cs`, `AppDbContext`) solo cambian con un PR pequeño de tipo `chore`, aprobado por quien mantiene el esqueleto.

## Commits

Prefijo obligatorio, mensaje en español, en imperativo, en una sola línea y sin punto final.

| Prefijo | Cuándo se usa | Ejemplo |
|---|---|---|
| `feat:` | Nueva funcionalidad | `feat: agregar generación de folio único para tickets` |
| `fix:` | Corrección de errores | `fix: corregir validación de serie duplicada en equipos` |
| `chore:` | Mantenimiento, dependencias y configuraciones | `chore: actualizar paquete Npgsql` |
| `refactor:` | Mejora de código sin cambiar la funcionalidad | `refactor: extraer cálculo de disponibilidad de repuestos` |
| `test:` | Pruebas unitarias o de integración | `test: agregar pruebas de transición de estado de tickets` |
| `docs:` | Documentación | `docs: documentar la configuración de PostgreSQL local` |

## Pull Requests

- El título usa el mismo formato que un commit (`feat: agregar listado de clientes`).
- Se rellena la plantilla que carga GitHub automáticamente.
- Debe indicar la etapa del flujo, cómo probarlo y los criterios de aceptación cubiertos.
- Definición de terminado, para cada tarea:
  - Compila y levanta en local (`dotnet run` para el backend y `npm run dev` para el frontend).
  - Commits con la convención de arriba.
  - Sin conflictos con la rama destino y revisado por al menos otra persona.
  - Cada criterio de aceptación probado a mano o con una prueba automática.

## Convenciones de código

### Backend (.NET 10 / C#)

- **Regla de dependencia:** `Domain` no depende de nada; `Application` depende de `Domain`; `Infrastructure` y `Backend` dependen de `Application`.
- **Controllers sin lógica de negocio:** reciben la petición, llaman a un servicio de `Application` y devuelven un `IActionResult`. No usan `DbContext` ni llevan try-catch.
- **Errores:** se lanzan excepciones de `Domain/Exceptions` (`NotFoundException`, `ConflictException`, `ValidationException`, `UnauthorizedException`, `ForbiddenException`, `BusinessRuleException`); el middleware global las convierte en respuestas `ProblemDetails`.
- **Inyección de dependencias:** toda clase terminada en `Service` o `Repository` se registra automáticamente contra su interfaz `I<NombreClase>`. No se edita `Program.cs` para registrar servicios. Los servicios van en `Application/Services` y los repositorios en `Infrastructure/Persistence/Repositories`.
- **Asincronía:** toda llamada a la base de datos es asíncrona y el método lleva el sufijo `Async`.
- **Nulos:** contexto `nullable` habilitado y respetado.
- **DTOs:** la API nunca devuelve entidades. Los DTOs viven en `Application/DTOs`, dentro de las carpetas `Request` y `Response`, como `record` con propiedades `init` (los records posicionales rompen la validación con atributos). Nombres: `Create<Entidad>Request`, `Update<Entidad>Request`, `<Entidad>Response` y `<Entidad>ListItemResponse`. Un archivo por DTO.
- **Usuario actual:** se obtiene con `ICurrentUserService`; no se lee el token a mano.
- **Autorización:** los endpoints exigen autenticación por defecto. Los roles se restringen con `[Authorize(Roles = "Administrator,Reception")]`.
- **Correos:** se guardan y se comparan en minúsculas.

### Nomenclatura

| Elemento | Convención | Ejemplo |
|---|---|---|
| Clases, records y métodos | `PascalCase` | `TicketService`, `GetTicketById` |
| Interfaces | `PascalCase` con prefijo `I` | `IUserRepository` |
| Variables locales y parámetros | `camelCase` | `ticketId`, `newClient` |
| Campos privados | `camelCase` con prefijo `_` | `_dbContext` |
| Constantes | `UPPER_SNAKE_CASE` | `MAX_IMAGE_COUNT` |
| Componentes de React | `PascalCase` | `TicketCard.tsx` |
| Funciones, hooks y utilidades | `camelCase` | `useAuth`, `formatCurrency` |
| Tipos e interfaces de TypeScript | `PascalCase` | `TicketProps` |

### Frontend (Next.js / TypeScript)

- Todo el código vive en `src/` y el enrutamiento solo en `src/app`.
- Los componentes son Server Components por defecto; `"use client"` solo va en componentes hoja que necesiten interactividad.
- Prohibido `any`: todo dato del backend tiene su interfaz TypeScript.
- Solo Tailwind CSS (los componentes de shadcn/ui son compatibles porque se escriben con Tailwind).
- Las llamadas a la API se centralizan en funciones asíncronas en `src/lib/api`, separadas de la vista.

### Base de datos (PostgreSQL)

- Entidades de C# en singular (`Ticket`, `Client`); tablas en plural gestionadas por EF Core (`Tickets`, `Clients`).
- Claves primarias `Id` de tipo `Guid`.
- Enumeraciones guardadas como texto y montos como `decimal(18,2)`.
- Sin borrado físico en registros con relaciones: se desactiva con `IsActive` o se cancela con motivo.
- Los cambios al modelo se hacen con migraciones de EF Core; no se editan las migraciones ya mergeadas en `develop`.
