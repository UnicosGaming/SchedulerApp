## Tools ##

This folder contains the script to replace a placeholder for the API KEY in any file required and `keys.txt` file that contains the key/s needed.

The file `keys.txt` is added to the .gitignore file to avoid to push to the repository.

Format of the `keys.txt` file:

```
KEY1:CE92C2C8-0B39-44B1-9798-11DD379B27C0
```

The files that need the API KEY value must have a placeholder that will be used to be replaced by the corresponding value.

Below there is an example of a file with the placeholder INSERT_KEY_HERE:
```
<?xml version="1.0" encoding="utf-8"?>
<manifest>
    <data android:scheme="INSERT_KEY_HERE" android:host="auth"/>
</manifest>
```

## How to use it ##

The script is called from the <em>pre-build</em> event in Visual Studio with the command:

```
powershell.exe -ExecutionPolicy Unrestricted $(SolutionDir)Tools\replace-keys-script.ps1 $(SolutionDir)Tools\keys.txt "KEY1" $(ProjectDir)Properties\SampleManifest.xml "INSERT_KEY_HERE"
```

<b>Note:</b> If the placeholder has # characters like #INSERT_KEY_HERE# they must be escaped with grave-accents: "\`#INSERT_KEY_HERE\`#" in order to escape the comment symbol.
