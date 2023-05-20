# XenForo Addon Hasher

A simple desktop tool used to generate hashes for XenForo addons.


## Why does this exist?

This tool is intended for XenForo administrators that don't usually do addon development, but may edit their addon source files for any reason.

This tool makes is easier to generate the `hashes.json` file that is required to install or update addons.


### Select Addon

Clicking `Open` will allow you to select the root directory for the addon you want to generate hashes for.

The addon root directory is usually named `upload` and must contain the contents of the addon to generate hashes for.

The directory selection will fail if it cannot find the sub-directory `/src/addons`, as in `./upload/src/addons`.


### Generating Hashes

Upon selecting a valid addon root directory, hashes are automatically generated and presented in Json format in the view panel.

Changes to the addon root files and directories will be automatically detected and hashes will be regenerated.

Clicking `Hash` will allow you to force regeneration of the hashes and Json output in the view panel.


### Exporting Hashes

Clicking `Copy` will allow you to copy the entire Json output to the clipboard so it can be pasted elsewhere.

Clicking `Save` will allow you to save the entire Json output to a file.
