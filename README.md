<h1 align="center">🏥 Sistema de Gestión de Turnos - API</h1>

<h3>API REST desarrollada en .NET para la gestión de turnos médicos. Permite administrar pacientes, profesionales y roles dentro del sistema.</h3>
<br>

> <h2>🚀 Funcionalidades</h2>
<ul>

<li>Autenticación de usuarios (login)</li><br>
<li>Sistema de roles:
Admin,
Recepcionista,
Profesional,
Paciente.</li><br>
<li>Gestión de turnos:
Solicitud de turnos
Cancelación
Visualización por usuario
Gestión de pacientes
Registro de atención por profesional
Visualización de turnos del día
</ul>

<br>

> <h2>🧠 Lógica del sistema</h2>

El sistema maneja distintos tipos de usuarios con permisos específicos:

<ul>
  <li>Pacientes: pueden solicitar, cancelar y ver sus turnos</li>
  <li>Profesionales: visualizan turnos asignados y registran atenciones</li>
  <li>Recepcionistas: gestionan turnos y pacientes</li>
  <li>Administradores: control total del sistema</li>
</ul>
<br>

> <h2>🛠️ Tecnologías</h2>

🔗 Frontend

Este proyecto está pensado para ser consumido por un frontend en React:

[👉 Repositorio Frontend](https://github.com/alanpisani/turnero-front)

<br>

> <h2>⚙️ Instalación</h2>
<br>
<pre>git clone https://github.com/alanpisani/turnero</pre>
<br>

El proyecto utiliza archivos de configuración por entorno.

Los datos sensibles (como connection strings y claves JWT) no se incluyen en el repositorio.

Para correr el proyecto en local, crear un archivo `secrets.json` en la raíz con la siguiente estructura:

<br>
<pre>
{
  "ConnectionStrings": {
    "connection": "TU_CONNECTION_STRING"
  },
  "Jwt": {
    "Key": "TU_CLAVE_SECRETA",
    "Issuer": "Turnero",
    "Audience": "TurneroClientes"
  }
}
</pre>
<br>

Una vez que está todo listo, ingresar esto en terminal:

<br>
<pre>dotnet run</pre>
<br>

> <h2>📌 Notas</h2>

<br>
Proyecto enfocado en la lógica de negocio y estructura de una API REST, pensado para ser consumido por aplicaciones frontend.
