rm *.nupkg
nuget pack .\FlockBuddyWidgets.nuspec -IncludeReferencedProjects -Prop Configuration=Release
cp *.nupkg C:\Projects\Nugets\