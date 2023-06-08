Este prefab lleva asociado un script que definirá unas rutas estándar para la estructura de los assets.
Consideraciones:
 - Añadir este prefab a la escena inicial
 - Automáticamente se colocará de los primeros scripts en el "Script Execution Order"
 - Se va a definir un contenedor dentro de "Assets", denominado "0_Standard_Folders"
 - A la nueva estructura de carpetas, no mover los contenidos de las carpetas Plugins, ThirdParty, u otras carpetas procedentes de Packages del Package Manager o de la Asset Store. Esto es porque, cuando se fuesen a actualizar, se crearían de nuevo en la ruta original de Assets.
 - Se pueden añadir nuevas rutas a la estructura, solo es una estructura base a seguir para unificar proyectos y facilitar el encontrar assets.