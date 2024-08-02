# <div align="center">ColoredNames<br><sub> A simple plugin to allow users to have colored names with no badge text, Also uses a database for storing data. Supports Cedmod and custom plugins! (Read below)<br>[![Github All Releases](https://img.shields.io/github/downloads/DentyTxR/ColoredNames/total.svg)]()</sub></div>

## NOTE: This plugin relies on EXILED permissions! The player/group NEEDS to have `colorednames.access` otherwise the plugin will not give the user a colored name when joining the server and cannot access commands.

<br>

# <div align="center">Permissions</div>

| Permission | Use |
| --- | --- |
| `colorednames.access` | Access to plugin and allows automaticlly getting colored name |
| `colorednames.addself` | Access to `addself` command |
| `colorednames.add` | Access to `add` command |
| `colorednames.changecolor` | Access to `changecolor` command |
| `colorednames.remove` | Access to `remove` command |
| `colorednames.cache` | Access to `cache` command |
| `colorednames.showdata` | Access to `showdata` command |

<br>

# <div align="center">Commands</div>

| Command Name | Does | Permission |
| --- | --- | --- |
| .colorednames | Parrent command | None |
| .cn | Parrent command | None |
| .cn addself | Adds the command sender to the database and gives colored name (this is made for VIP members or donators, this sets OverrideBadge to true) | colorednames.addself |
| .cn changecolor | Changes the command senders color, if they are in the database | colorednames.changecolor |
| .cn add | Adds a user either by RA ID or steam64ID (this is made more for the owner/staff) | colorednames.add |
| .cn remove | Removes a user from the database (data.yml) | colorednames.remove |
| .cn cache | Re-caches database if you manually modifed data.yml | colorednames.cache |
| .cn showdata | Shows all users and data in data.yml | colorednames.showdata |


<br>

# <div align="center">If You Use A Custom Patreon/Donator System OR CEDMOD</div>
### Cedmod
- If you use cedmod and cedmod rolesync for donators/patreons you need to give the role the `colorednames.access` permission, This allows the player to get a colored name when joining the server, Then if they lose a Discord role with the tied permission they will no longer get a colored role.

### Other system/custom plugin
- Depending on how your plugin works, If you have a method that handles removing a person you should be able to use the following method `ColoredNames.Plugin.Singleton.DatabaseHandler.RemoveUser(userid string here)`

<br>

# <div align="center">FAQ</div>

- Data is saved at `/EXILED/Configs/ColoredNames/data.yml`

- OverrideBadge works as follows:
  - if the player does not have any server roles, OverrideBadge doesnt effect them. they will get a colored name.
  - if the player DOES have a server role, such as 'moderator' and OverrideBadge is set to false they will not get a colored name.
  - if the player DOES have a server role, and OverrideBadge is set to true, the colored name completely overrides their badge color and text, permissions are not effected.

 - If you manually modify `data.yml` you NEED to recache the plugins cache by running `.cn cache`

<br><br><br>

This is still a work in progress, I rarely do stuff related to data so the code isnt the best, Everything does work, I will make changes and additions in the future that will at least not break data.yml so user data will not be lost.
