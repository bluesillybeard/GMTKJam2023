#!/bin/bash
#NOTE: This only exports the Linux version since dotnet doesn't support cross-platform AOT.

# Publish the project with dotnet
dotnet publish -c Release -o output/linux-x64 --os linux -a x64 --sc
# Copy over the assets folder
rsync -r assets/ output/linux-x64/assets/
# Test to make sure the exported project runs
cd output/linux-x64
./GMTKJam2023


