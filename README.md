# AppServiceConnection_Troubleshoot
To Build, set configuration to X64
Make sure the package's entrypoint is Package_Launcher

Some projects require to manually add references. On a windows 10 machine, they can be found in the following locations:
for projects:

FULLTRUST_NET_FRAMEWORK;
 C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.17763.0\Windows.winmd

Package_Launcher
C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.17763.0\Facade\windows.winmd
C:\Program Files (x86)\Windows Kits\10\References\10.0.17763.0\Windows.Foundation.FoundationContract\3.0.0.0\Windows.Foundation.FoundationContract.winmd
C:\Program Files (x86)\Windows Kits\10\References\10.0.17763.0\Windows.Foundation.UniversalApiContract\7.0.0.0\Windows.Foundation.UniversalApiContract.winmd
