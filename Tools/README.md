## Tools ##

This folder contains the script to replace a placeholder for the API KEY in any file required. The keys are storage in an Environment variable

The files that need the API KEY value must have a placeholder that will be used to be replaced by the corresponding value.

Below there is an example of a file with the placeholder INSERT_KEY_HERE:
```
<?xml version="1.0" encoding="utf-8"?>
<manifest>
    <data android:scheme="INSERT_KEY_HERE" android:host="auth"/>
</manifest>
```

## How to use it ##

```
replace-keys-script.ps1 "Key_Name" File_path_to_insert_the_key_value "placeholder"
```

The script is called from the <em>pre-build</em> event in Visual Studio with the command:

```
powershell.exe -ExecutionPolicy Unrestricted $(SolutionDir)Tools\replace-keys-script.ps1 $(SolutionDir)Tools\keys.txt "KEY1" $(ProjectDir)Properties\SampleManifest.xml "INSERT_KEY_HERE"
```

### Notes ###
* If the placeholder has # characters like #INSERT_KEY_HERE# they must be escaped with grave-accents: "\`#INSERT_KEY_HERE\`#" in order to escape the comment symbol.

* This script only replaces one key per file. In case we need to replace several keys we need to call the script several times in the <em>pre-build</em> event.

* Because the script changes the content of the file, Git will mark this file as changed. An option to avoid this is call a `checkout` from the <em>post-build</em> event.

        git checkout $(ProjectDir)Properties\SampleManifest.xml
