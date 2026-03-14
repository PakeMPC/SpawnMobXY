# ESPAÑOL (ES)

**SpawnMobXY** es un plugin avanzado y altamente optimizado para servidores de TShock 6 (Terraria 1.4.5). Permite a los administradores invocar NPCs con propiedades personalizadas, drops específicos y comportamientos de IA modificados. 

Originalmente un fork de SpawnMobX, esta versión ha sido reescrita y se ha incluido un **Sistema de Plantillas Globales (Attach)** y un sistema de drops personalizados.


## ✨ Características Principales
* **Drops Personalizados**: Asigna ítems específicos, cantidades y probabilidades de drop a cualquier mob invocado.
* **Plantillas Globales (`attach`)**: Fija las estadísticas para NPCs específicos. Una vez fijado, *cada* mob de ese tipo (ya sea invocado por comandos o que aparezca naturalmente en el mundo) heredará automáticamente tu configuración de vida, IA y drops.
* **Soporte de Consola**: Ejecuta comandos de aparición de forma segura desde la consola del servidor (requiere indicar las coordenadas `x` e `y` explícitamente).
* **Multidioma**: Soporte nativo para Español (`es`), Inglés (`en`) y Portugués (`pt`).


## 📦 Instalación
1. Descarga el último archivo `SpawnMobXY.dll` desde la sección de *Releases*.
2. Coloca el plugin dentro de tu carpeta `ServerPlugins/`.
3. Reinicia el servidor. El plugin generará automáticamente el archivo `SMX_Config.json` en tu carpeta de TShock.
4. Otorga el permiso `spawnmobx.use` a los grupos de usuarios que desees que tengan acceso al comando.


## 📝 Comandos y Sintaxis

### Spawn Inmediato
`/smx <nombre del mob o id> [cantidad] [modificadores...]`
Invoca la cantidad especificada de mobs a 25 bloques de distancia frente al jugador. Si se ejecuta desde la consola, los modificadores de ubicación (`x` e `y`) son obligatorios.

*Ejemplo:* `/smx "Demon Eye" 5 health=3000 ai0=5 drop=73:1:50`
*(Invoca 5 Ojos Demoníacos con 3000 de HP, la IA modificada, y un 50% de probabilidad de soltar 1 Moneda de Oro).*

### Plantillas Globales (Sistema Attach)
`/smx attach <nombre del mob o id> [modificadores...]`
Guarda los modificadores de forma persistente en `SMX_Config.json`. Cualquier mob futuro de este tipo aplicará estas estadísticas de forma automática.

*Ejemplo:* `/smx attach "Green Slime" health=500 drop=75:3:100`
*(Transforma a todos los Slimes Verdes del servidor en slimes de 500 HP que siempre sueltan 3 Estrellas Caídas al morir).*

### Comandos de Gestión
* `/smx list`: Muestra la lista en el chat de todos los mobs que tienen estadísticas fijadas actualmente.
* `/smx detach <nombre del mob o id>`: Elimina la plantilla global para el mob especificado, devolviéndolo a la normalidad.


## ⚙️ Modificadores

Los modificadores se añaden después del nombre del mob y la cantidad, separados por espacios y delimitados por el signo `=`.

| Modificador | Descripción | Sintaxis |
| :--- | :--- | :--- |
| **`health`** | Vida máxima del NPC. | `health=5000` |
| **`drop`** | Drop personalizado para este NPC. | `drop=idItem:cantidad:probabilidad` (ej. `drop=117:1:100`) |
| **`x`** | Coordenada X (en bloques/tiles, como lo muestra el comando `/pos`). | `x=2500` |
| **`y`** | Coordenada Y (en bloques/tiles, como lo muestra el comando `/pos`). | `y=1200` |
| **`ai0` a `ai3`**| Número decimal (float) que altera varios comportamientos de la Inteligencia Artificial del NPC. | `ai0=2.5` |

*(Nota: El modificador de nombre (`name`) fue eliminado deliberadamente en esta versión, ya que el cliente de Terraria 1.4.5 sobrescribe forzosamente los nombres personalizados de los NPCs comunes basándose en los archivos de idioma locales del jugador).*


## 📂 Configuración (`SMX_Config.json`)

Al iniciarse por primera vez, el plugin creará un archivo de configuración:
```json
{
  "Language": "en",
  "AttachedMobs": []
}
```

------
# ENGLISH (EN)

**SpawnMobXY** is an advanced and highly optimized plugin for TShock 6 (Terraria 1.4.5) servers. It allows administrators to spawn NPCs with custom properties, specific drops, and modified AI behaviors. 

Originally a fork of SpawnMobX, this version has been rewritten to include a **Global Templates System (Attach)** and a custom drops system.

## ✨ Key Features
* **Custom Drops**: Assign specific items, quantities, and drop chances to any spawned mob.
* **Global Templates (`attach`)**: Fixes stats for specific NPCs. Once attached, *every* mob of that type (whether spawned by commands or appearing naturally in the world) will automatically inherit your health, AI, and drop configurations.
* **Console Support**: Safely execute spawn commands from the server console (explicitly requires `x` and `y` coordinates).
* **Multi-language**: Native support for Spanish (`es`), English (`en`), and Portuguese (`pt`).

## 📦 Installation
1. Download the latest `SpawnMobXY.dll` file from the *Releases* section.
2. Place the plugin inside your `ServerPlugins/` folder.
3. Restart the server. The plugin will automatically generate the `SMX_Config.json` file in your TShock folder.
4. Grant the `spawnmobx.use` permission to the user groups you want to have access to the command.

## 📝 Commands and Syntax

### Immediate Spawn
`/smx <mob name or id> [amount] [modifiers...]`
Spawns the specified amount of mobs 25 blocks in front of the player. If executed from the console, the location modifiers (`x` and `y`) are mandatory.

*Example:* `/smx "Demon Eye" 5 health=3000 ai0=5 drop=73:1:50`
*(Spawns 5 Demon Eyes with 3000 HP, modified AI, and a 50% chance to drop 1 Gold Coin).*

### Global Templates (Attach System)
`/smx attach <mob name or id> [modifiers...]`
Persistently saves the modifiers in `SMX_Config.json`. Any future mob of this type will automatically apply these stats.

*Example:* `/smx attach "Green Slime" health=500 drop=75:3:100`
*(Transforms all Green Slimes on the server into 500 HP slimes that always drop 3 Fallen Stars upon death).*

### Management Commands
* `/smx list`: Displays a chat list of all mobs that currently have fixed stats.
* `/smx detach <mob name or id>`: Removes the global template for the specified mob, returning it to normal.


## ⚙️ Modifiers

Modifiers are added after the mob name and amount, separated by spaces and delimited by the `=` sign.

| Modifier | Description | Syntax |
| :--- | :--- | :--- |
| **`health`** | Maximum HP of the NPC. | `health=5000` |
| **`drop`** | Custom drop for this NPC. | `drop=itemId:amount:chance` (e.g. `drop=117:1:100`) |
| **`x`** | X Coordinate (in tiles, as shown by the `/pos` command). | `x=2500` |
| **`y`** | Y Coordinate (in tiles, as shown by the `/pos` command). | `y=1200` |
| **`ai0` to `ai3`**| Decimal number (float) that alters various behaviors of the NPC's Artificial Intelligence. | `ai0=2.5` |

*(Note: The name modifier (`name`) was deliberately removed in this version, as the Terraria 1.4.5 client forcefully overrides custom names for common NPCs based on the player's local language files).*


## 📂 Configuration (`SMX_Config.json`)

Upon starting for the first time, the plugin will create a configuration file:
```json
{
  "Language": "en",
  "AttachedMobs": []
}
