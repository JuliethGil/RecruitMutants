# Reclutamiento de mutantes - Mercadolibre

Proyecto para descubrir basado en la cadena del adn cuales pertenecen a mutantes.


## Ejecucion Local

A continuacion se explicara lo necesario para poder ejecutar el proyecto de manera local y realizar las pruebas.

### Pre-requisitos

• Visual Studio 2019 https://visualstudio.microsoft.com/es/downloads/ 
• Base de datos PostgreSql versión 13 https://www.postgresql.org/download/ 
• Herramienta para consumir api se recomienda Postman https://www.postman.com/downloads/ 
• Solicitar acceso al repositorio de Github
• .Net Core 3.1 o superior
• JMeter
• Docker

### Instalación 

-Crear una base de datos y guardar el nombre y contraseña, se necesitara para mas adelante
-Crear la tabla **DnaSequences**
- Abrir el proyecto con VS 2019 y buscar el archivo **appsettings.json** dentro del proyecto **RecruitMutants**
- En la etiqueta **DefaultConnection** remplazar el nombre de la base de datos (database) y contraseña(password ) por los valores creados anteriormente.

### Configuracion Base de datos


Script para crear la tabla **DnaSequences**

```
CREATE TABLE public.DnaSequences
(
    "Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 ),
    "PersonDna" text NOT NULL,
    "IsMutant" boolean NOT NULL, 
    CONSTRAINT DnaSequences_pkey PRIMARY KEY ("Id"));

ALTER TABLE public.DnaSequences
    OWNER to postgres;

```

## Ejecutando las pruebas Unitarias

En la consola de Package Manager ejecutar los siguientes comandos:

```
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput='../Documentation/TestCoverage/'
```

El valor **CoverletOutput** es el lugar donde se generara un documento .xml

Luego el siguiente comando para generar una pagina web que mostrará a detalle la cobertura:

```
reportgenerator.exe "-reports:Documentation/TestCoverage\coverage.cobertura.xml" "-targetdir:Documentation/TestCoverage\html" "-reporttypes:HTML;"
```

El valor de **reports** debe ser la misma la carpeta donde quedo guardado el archivo con el nombre generado en el primer paso.


## Ejecutando las pruebas estres
 
Para las pruebas se usara **Jmeter**

Abrir el programa y abrir el archivo de pruebas que se encuenta en *Documentacion/Jmeter*


## Despliegue

El despliegue se realiza en Heroku.

Buildear el proyecto con DockerFile

```
docker build -t recruitmutants .
```
Realizar el push de la nueva version a heroku
```
heroku container:push web -a recruitmutants
```

Hacer el despliegue al servidor
```
heroku container:release web -a recruitmutants
```

##Ejecucion en la nube

https://recruitmutants.herokuapp.com/

##Endpoints


###Enviar cadena de adn
```
POST   /api/mutants
```
Ejemplo de parametro

```
{
  "dna": ["ATGA","CAGTGC","TTATGT"]
}
```

Retorna respuesta estándar HTTP 200 OK en caso de ser mutante de lo contrario 403 Forbidden


###Obtener estadisticas
```
POST   /api/stats
```

Ejemplo de respuesta 
```
{
    "count_mutant_dna": 3,
    "count_human_dna": 4,
    "ratio": 0.8
}
```
Adicional la respuesta estándar HTTP 200 OK

