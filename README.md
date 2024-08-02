# STILL WIP, IM NOT FINISHED BUT IT WORKS, DATA.YML STUFF WONT CHANGE SO EXISTING DATA WONT BREAK
# ColoredNames

### A simple plugin to allow users to have colored names, this is done by giving the player invisible rank text, Also uses a yml file as a database.

[![Github All Releases](https://img.shields.io/github/downloads/DentyTxR/ColoredNames/total.svg)]()

Data is saved at /EXILED/Configs/ColoredNames/data.yml

OverrideBadge works as follows:
- if the player does not have any server roles, OverrideBadge doesnt effect them. they will get a colored name.
- if the player DOES have a server role, such as 'moderator' and OverrideBadge is set to they will not get a colored name.
- if the player DOES have a server role, and OverrideBadge is set to true, the colored name completely overrides their badge color and text, permissions are not effected.

Commands

| Command Name | Does | Permission |
| --- | --- | --- |
| .colorednames | Parrent command | None |
| .cn | Parrent command | None |
| .cn addself | Adds the command sender to the database and gives colored name (this is made for VIP members or donators) | colorednames.addself |
| .cn add | Adds a user either by RA ID or steam64ID (this is made more for the owner/staff) | colorednames.add |
| .cn remove | Removes a user from the database (data.yml) | colorednames.remove |
| .cn cache | Re-caches database if you manually modifed data.yml | colorednames.cache |
| .cn showdata | Shows all users and data in data.yml | colorednames.showdata |
