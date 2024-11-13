# Prueba Técnica COINK

El propósito de este proyecto es la realización de la prueba técnica planteada por COINK como parte del proceso de selección para el cargo de Desarrollador BackEnd.

## Description

La prueba técnica consite en la evaluación de dos partes: Conocimientos de Base de datos relacionales y desarrollo en C#.

Se debe implementar servicios web api rest para la administración de las entidades: Pais, Departamento, Municipio y Usuario.

## Presentación

### Dependencias

* .NET Framework 8.0.
* Entity Framework 8.0
* Npgsql.EntityFrameworkCore.PostgreSQL 8.0

### Instalación

* Para el desarrollo de esta prueba, se instaló Postgres 17.0, es menester tener una versió instalada de postgres en un servidor determiando
* Descargar el código en el lugar deseado.
* Una vez descargado el código, sedebe proceder a abrir la solución CAFEMACA.Coink.PruebaTecnica.sln
* abierta la solución, se debe modificar el string de conexión a la base de datos deseada. esto se realiza en el archivo appsettings.json y la entrada CAFEMACA.Coink.PruebaTecnicaDbConn

### Executing program

* How to run the program
* Step-by-step bullets
```
code blocks for commands
```

## Help

Any advise for common problems or issues.
```
command to run if program contains helper info
```

## Authors

Desarrollador

Carlos Fernando Malagón Cano  
[cmalagon@uniandes.edu.co](mailto:cmalagon@uniandes.edu.co)
LinkedIn: [https://www.linkedin.com/in/cmalagon/](https://www.linkedin.com/in/cmalagon/)

## Version History

* 0.2
    * Various bug fixes and optimizations
    * See [commit change]() or See [release history]()
* 0.1
    * Initial Release

## License

This project is licensed under the [NAME HERE] License - see the LICENSE.md file for details

## Conocimientos

Para el desarrollo de este proyecto se utiliz162
* La solución se plantea bajo el manejo del concepto de CLEAN ARCHITECTURE, utilizando un template de Visual Studio desarrollado por el propio autor de este desarrollo de la prueba técnica.
* Manejo de Patreones de Desarrollo como: Options Pattern, Result Pattern, Repository Patter, UnitOfWork Pattern
* Como buenas prácticas igualmente se maneja procesos como: Health Check, Logging con Serilog
* Como motor de base de datos se utiliza POSTGRES
