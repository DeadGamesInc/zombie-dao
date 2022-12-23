#!/bin/bash
cloc --exclude-dir="$(sed -z 's/\n/,/g;s/,$/\n/' .clocignore)" --exclude-ext=txt,sln,csproj,md,json --quiet .