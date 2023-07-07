#!/bin/bash
#NOTE: this script is designed to run on linux.
# If you are using windows, typing the dotnet commands yourself can't be that hard, can it?

# Publish the project with dotnet
dotnet publish -c Release -o output/win-x64 --os win -a x64 --sc
dotnet publish -c Release -o output/linux-x64 --os linux -a x64 --sc
# Copy over the assets folder
rsync -r assets/ output/win-x64/assets/
rsync -r assets/ output/linux-x64/assets/
# Test to make sure the exported projects run
# First, test the linux build
cd output/linux-x64
./GMTKJam2023
#Then test the windows one, using wine
# I don't see why one might work while the other doesn't, but I think it's worth testing both anyway.
cd ../win-x64
wine GMTKJam2023.exe

