# Automatización de cálculos del laboratorio de presión hidrostática

Proyecto final de la asignatura **Introducción a la Programación**.

## Descripción general

Este proyecto consiste en un programa desarrollado en **C#** para automatizar los cálculos del laboratorio de **presión hidrostática** de Mecánica de Fluidos.

El sistema permite registrar mediciones experimentales, validar datos ingresados por el usuario, calcular automáticamente las variables requeridas del laboratorio y guardar los resultados en un archivo CSV para facilitar su uso en informes académicos.

La finalidad del proyecto es reducir el tiempo empleado en cálculos repetitivos, disminuir errores de transcripción o aplicación de fórmulas y permitir que los estudiantes dediquen más tiempo al análisis e interpretación de los resultados.

## Integrantes

* Ronaldo Jeshua Castillo Cuadra
* Mariela Fernanda Hurtado Duarte
* Nijeri Iveth Jarquín García
* Julio Javier Sevilla Gallegos

## Funcionalidades principales

* Registro de hasta **15 mediciones**.
* Validación de entradas numéricas positivas.
* Cálculo automático de:

  * Fp: fuerza sustitutiva.
  * Ip: brazo de palanca.
  * Mp: momento hidrostático.
  * Mw: momento antagónico.
  * Error porcentual.
* Visualización de resultados en consola.
* Eliminación de mediciones registradas.
* Guardado de resultados en archivo CSV.
* Lectura automática de mediciones guardadas.
* Presentación de créditos del equipo al finalizar.

## Datos utilizados por el sistema

### Datos ingresados por el usuario

| Variable | Descripción       | Unidad |
| -------- | ----------------- | ------ |
| Iw       | Distancia a pesas | mm     |
| Fw       | Fuerza de pesas   | N      |
| s        | Nivel del agua    | mm     |

### Constantes utilizadas

| Constante | Descripción                |  Valor | Unidad |
| --------- | -------------------------- | -----: | ------ |
| rho       | Densidad del agua          | 1000.0 | kg/m³  |
| g         | Aceleración de la gravedad |   9.81 | m/s²   |
| b         | Ancho de la superficie     |  0.075 | m      |
| L         | Altura de la placa         |    0.1 | m      |

## Tecnologías utilizadas

* Lenguaje: **C#**
* Entorno recomendado: **Visual Studio**
* Control de versiones: **Git y GitHub**
* Formato de salida: **CSV**

## Instrucciones de instalación y ejecución

1. Clonar el repositorio:

```bash
git clone https://github.com/juliosevillag04/presion_hidr.git
```

2. Abrir el proyecto en **Visual Studio**.

3. Abrir la solución del proyecto:

```text
presion_hidr.sln
```

4. Ejecutar el programa.

5. Utilizar el menú principal para seleccionar una opción:

```text
1. Agregar medición
2. Mostrar mediciones
3. Eliminar medición
4. Guardar archivo
5. Salir
```

6. Al guardar, el programa genera el archivo:

```text
mediciones_hidrostaticas.csv
```

El archivo se guarda automáticamente en la carpeta **Descargas** del usuario.

## Estructura del proyecto

```text
presion_hidr/
│
├── README.md
├── presion_hidr.sln
│
├── presion_hidr/
│   ├── Program.cs
│   └── presion_hidr.csproj
│
├── Documentacion/
│   ├── Documento del proyecto
│   ├── Presentación
│   ├── Diagrama de flujo
│   └── Diagrama de estructura
│
└── Evidencias/
    ├── Encuesta
    ├── Entrevistas
    └── Capturas del programa
```

## Ramas del repositorio

El repositorio evidencia el trabajo colaborativo mediante ramas individuales por integrante:

```text
main
develop
feature/ronaldo
feature/mariela
feature/nijeri
feature/julio
```

Cada rama contiene aportes relacionados con el desarrollo del sistema, sus módulos, documentación o evidencias del proyecto.

## Módulos principales del programa

| Módulo / función       | Responsabilidad                                       |
| ---------------------- | ----------------------------------------------------- |
| menu()                 | Muestra las opciones y lee la elección del usuario.   |
| pedirDatos()           | Registra datos, valida entradas y calcula resultados. |
| mostrarDatos()         | Presenta las mediciones en una tabla.                 |
| eliminarMedicion()     | Elimina una medición seleccionada por el usuario.     |
| guardarArchivo()       | Guarda los registros en un archivo CSV.               |
| leerArchivo()          | Carga datos previamente guardados.                    |
| procesarLineaArchivo() | Valida y convierte cada línea del CSV.                |
| mostrarCreditos()      | Muestra los integrantes al finalizar.                 |

## Repositorio

https://github.com/juliosevillag04/presion_hidr

## Estado del proyecto

Proyecto académico finalizado para entrega y evaluación en la asignatura **Introducción a la Programación**.
