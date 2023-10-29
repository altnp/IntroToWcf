# Prerequisists
- Visual Studio 2022
- SOAPUI

## Configure your Visual Studio instance
From the root of this repository, run the following powershell script. If you setup your pc using the `init-pc` repository you can skip this step.


```
function Search-VS {
    foreach ($d in @("C:\Program Files (x86)\Microsoft Visual Studio", "C:\Program Files\Microsoft Visual Studio")) {
        foreach ($e in @("2022\Professional", "2022\Community")) {
            if (Test-Path "$d\$e\Common7\IDE\devenv.exe") { return "$d\$e" }
        }
    }
    return $null
}

$vsPath = Search-VS
Start-Process "C:\Program Files (x86)\Microsoft Visual Studio\Installer\vs_installer.exe" -ArgumentList "modify --installPath `"$vsPath`" --config .\.vsconfig --passive" -Wait -NoNewWindow

```
