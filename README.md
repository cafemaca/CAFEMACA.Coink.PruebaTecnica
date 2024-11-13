# Prueba Técnica COINK

El propósito de este proyecto es la realización de la prueba técnica planteada por COINK como parte del proceso de selección para el cargo de Desarrollador BackEnd.

## Description

La prueba técnica consite en la evaluación de dos partes: Conocimientos de Base de datos relacionales y desarrollo en C#.

Se debe implementar servicios web api rest para la administración de las entidades: Pais, Departamento, Municipio y Usuario.

## Presentación
Para la presentación de la solución se tienen los requerimeintos solicitados:

### Base de Datos
La base de datos construída para la solución a nivel de tablas, se presenta el siguiente modelo enitdad/relación:
![Modelo E/R](/images/CoinkDB.png)

### Dependencias
* .NET Framework 8.0.
* Entity Framework 8.0
* Npgsql.EntityFrameworkCore.PostgreSQL 8.0

### Instalación

* Para el desarrollo de esta prueba, se instaló Postgres 17.0, es menester tener una versió instalada de postgres en un servidor determiando
* Descargar el código en el lugar deseado.
* Una vez descargado el código, sedebe proceder a abrir la solución CAFEMACA.Coink.PruebaTecnica.sln
* abierta la solución, se debe modificar el string de conexión a la base de datos deseada. esto se realiza en el archivo appsettings.json y la entrada CAFEMACA.Coink.PruebaTecnicaDbConn

### Ejecución del servicio Web Api Rest

* Desde Visual Studio 2022 o Visual Studio Code se puede realizar su ejecución
* Como en el desarrollo de esta prueba técnica se ha utilizado Entity Framework, la solución ha implementado que si se ejecuta por primera vez, este crea la basde de datos y sus respectivas tablas. Es decir, no es necesario previamente ejecurtar script de creación.

## Autores

Desarrollador

Carlos Fernando Malagón Cano  
[cmalagon@uniandes.edu.co](mailto:cmalagon@uniandes.edu.co)
LinkedIn: [https://www.linkedin.com/in/cmalagon/](https://www.linkedin.com/in/cmalagon/)

## Licencia

Esta solución es parte del cumplimiento de la prueba técnica propuesta para el cargo de Desarrollador BackEnd propuesto por Coink.
Por ende, está prohibida su utilización y reproducción para otros fines diferentes al planteado en esta prueba técnica.

## Conocimientos

Para el desarrollo de este proyecto se utilizó:
* La solución se plantea bajo el manejo del concepto de CLEAN ARCHITECTURE, utilizando un template de Visual Studio desarrollado por el propio autor de este desarrollo de la prueba técnica.
* Manejo de Patreones de Desarrollo como: Options Pattern, Result Pattern, Repository Patter, UnitOfWork Pattern
* Como buenas prácticas igualmente se maneja procesos como: Health Check, Logging con Serilog
* Como motor de base de datos se utiliza POSTGRES
