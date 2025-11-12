# 🎟️ HelpDesk FullStack

Aplicación **FullStack de HelpDesk** desarrollada con **.NET 8 (Backend)** y **Angular 20 (Frontend)**.  
Permite la **gestión de tickets de soporte**: creación, visualización, edición y eliminación de tickets.

---

## 🚀 Características principales

- 📋 Gestión completa de tickets (CRUD).  
- 👤 Sistema de autenticación básico (usuario/contraseña).  
- 🔗 Integración entre Backend .NET y Frontend Angular.  
- 🧱 Arquitectura limpia y escalable.

---

## ⚙️ Requisitos previos

Antes de ejecutar el proyecto, asegúrate de tener instaladas las siguientes herramientas:

| Herramienta | Versión mínima | Recomendado |
|--------------|----------------|--------------|
| **Git** | Cualquiera | Última versión |
| **.NET SDK** | 8.0 | 8.0.x |
| **Node.js** | >=18.13.0 | 18.19.0 LTS |
| **npm** | >=9.0.0 | 9.7.2 |
| **Angular CLI** | >=20.0.0 | 20.3.9 |

---

### 🔍 Verificaciones rápidas

Ejecuta los siguientes comandos en tu terminal para verificar versiones:

```bash
git --version
dotnet --version
node -v
npm -v
ng version

```




Si no tienes Angular CLI, instálalo globalmente:

```bash 
npm install -g @angular/cli
```

🧱 1️⃣ Clonar el repositorio
```bash
git clone https://github.com/NandoIbass/sistema_ticket.git
```



💡 Si tienes problemas con este comando, asegúrate de que Git esté configurado correctamente en tus variables de entorno.

🧩 2️⃣ Configurar y ejecutar el Backend (.NET API)

2.1Abre el proyecto clonado en Visual Studio codeo 

2.2 En la terminal de visual studio code, muévete a la carpeta del backend:

```bash 
cd backend/HelpDesk.Api

```


2.3 Restaura las dependencias:
```bash 
dotnet restore
```


2.4 Asegúrate de tener instalada la herramienta de Entity Framework CLI:
```bash 
dotnet tool install --global dotnet-ef --version 8.*
```


2.5 Ejecuta las migraciones para crear la base de datos:
```bash 
dotnet ef database update -p ../HelpDesk.Infrastructure -s .
```


2.6 Inicia el servidor backend:
```bash 
dotnet run
```



Si todo está correcto, deberías ver un mensaje similar a:
```bash 
Now listening on: http://localhost:5070
```

🖥️ 3️⃣ Configurar y ejecutar el Frontend (Angular)

3.1 Abre una nueva terminal (sin cerrar la del backend).

3.2 Entra en el directorio del frontend:
```bash 
cd frontend/helpdesk-frontend
```


3.3 Instala las dependencias:
```bash 
npm install
```


3.4 Inicia el servidor de desarrollo:
```bash 
npm start
```


Por defecto, la aplicación se ejecutará en:
```bash 
http://localhost:4200
```

***IMPORTANTE***

🔑 Inicio de sesión

Para acceder al sistema:
| Usuario | Contraseña |
|---------|------------|
| **admin** | **123456** |











