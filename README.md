# Bot Launcher

Bot Launcher was created to let you start, restart, and update an application (specifically Discord Bots)

## Config

The config.json file will be created after you first run the application

#### Config

| Config Value         | Type                                  | Description                                                                   |
|:---------------------|:--------------------------------------|:------------------------------------------------------------------------------|
| `path-to-executable` | `string`                              | Path to the folder containing the executable                                  |
| `executable`         | `string`                              | Name of the executable                                                        |
| `exit-codes`         | [`ExitCodesConfig`](#exitcodesconfig) | Configuration for what exit codes do what. [See more](#exitcodesconfig)       | 
| `archive-to-extract` | `string`                              | Name of the zip file that will be extracted when the update exitcode is given |
| `debug`              | `bool`                                | Whether Debug logs should be displayed                                        |

#### ExitCodesConfig

| Config Value        | Type   | Description                                                                               |
|:--------------------|:-------|:------------------------------------------------------------------------------------------|
| `restart`           | `int`  | The Launcher will restart the executeable when this was the exitcode                      |
| `shutdown`          | `int`  | The Launcher won't restart the executable when this was the exitcode                      |
| `unzip-and-restart` | `int`  | The Launcher will extract the specified archive and then restart the executable           |
| `restart-unknown`   | `bool` | Whether the Launcher should restart the executable when the exitcode is none of the above |

