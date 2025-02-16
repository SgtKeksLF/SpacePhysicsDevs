# Contribution and Development



## Setup your environment 

**Unity-Version** 
2022.3.45f1

**Required Unity-Packages** 
- Autodesk FBX SDK for Unity 4.2.1
- Code Coverage 1.2.6
- Custom NUnit 1.0.6
- Editor Coroutines 1.0.0
- FBX Exporter 4.2.1
- Input System 1.7.0
- JetBrains Rider Editor 3.0.31
- Mathematics 1.2.6
- Oculus XR Plugin 4.2.0
- OpenXR Plugin 1.12.0
- ProBuilder 5.2.3
- Profile Analyzer 1.2.2
- Settings Manager 2.0.1
- Test Framework 1.1.33
- TextMeshPro 3.0.6
- Timeline 1.7.6
- Unity UI 1.0.0
- Version Control 2.5.2
- Visual Scripting 1.9.4
- Visual Studio Code Editor 1.2.5
- XR Core Utilities 2.3.0
- XR InteractionToolkit 2.6.3
- XR Legacy Input Helpers 2.1.10
- XR Plugin Management 4.5.0



## Software Documentation

**UML Diagrams**

Find UML Diagrams for all used scripts under this link: 
[UML_Diagrams](https://drive.google.com/drive/folders/1FVS6ySN0AkKZb6CqeNkFKzxTq0ga8DLR?usp=sharing)


**Assets Folder Structure**

Each room of the game has its own asset folder named after it. In each of these room folders
there are forlders that sort the assets after their type like audio, scripts, prefabs etc.

See an overview here, note that mainly the Asset folder is important:

📂 SpacePhsyicsDev
├── 📂 .vscode
├── 📂 Assets
    ├── 📂 Assets_MainStation
        ├── 📂Audio
        ├── 📂Displays
        ├── 📂FBXFiles
        ├── 📂Material
        ├── 📂Prefabs
        ├── 📂Scripts
    ├── 📂 Assets_Mars_Room
        ├── 📂Audio
        ├── 📂Displays
        ├── 📂FBXFiles
        ├── 📂Material
        ├── 📂Prefabs
        ├── 📂Scripts  
    ├── 📂 Assets_Mercury_Room
        ├── 📂Audio
        ├── 📂Displays
        ├── 📂FBXFiles
        ├── 📂Material
        ├── 📂Prefabs
        ├── 📂Scripts    
    ├── 📂 Assets_Saturn_Room 
        ├── 📂Audio
        ├── 📂Displays
        ├── 📂FBXFiles
        ├── 📂Material
        ├── 📂Prefabs
        ├── 📂Scripts   
    ├── 📂 Assets_Venus_Room 
        ├── 📂Audio
        ├── 📂Displays
        ├── 📂FBXFiles
        ├── 📂Material
        ├── 📂Prefabs
        ├── 📂Scripts  
    ├── 📂 AssetsTutorialRoom
        ├── 📂Audio
        ├── 📂Displays
        ├── 📂FBXFiles
        ├── 📂Material
        ├── 📂Prefabs
        ├── 📂Scripts 
    ├── 📂 Global_Assets
        ├── 📂Audio
        ├── 📂Displays
        ├── 📂FBXFiles
        ├── 📂Material
        ├── 📂Prefabs
        ├── 📂Scripts
    ├── 📂 ProBuilder Data 
    ├── 📂 Samples
        ├── 📂XR Interaction Toolkit
            ├── 📂2.6.3
                ├── 📂Starter Assets
                    ├── 📂Animations
                    ├── 📂DemoSceneAssets
                        ├── 📂AffordanceThemes
                        ├── 📂Audio
                        ├── 📂Models
                        ├── 📂Prefabs
                            ├── 📂Climb
                            ├── 📂Interactables
                            ├── 📂Teleport
                            ├── 📂UI
                        ├── 📂Scripts
                        ├── 📂Settings
                        ├── 📂Sprites
                    ├── 📂Editor
                        ├── 📂Scripts
                    ├── 📂Filters
                    ├── 📂Materials
                    ├── 📂Models
                    ├── 📂Prefabs
                        ├── 📂Controllers
                        ├── 📂Interactors
                        ├── 📂Teleport
                    ├── 📂Presets
                    ├── 📂Scripts
                    ├── 📂Shaders
                    ├── 📂Tunneling Vignette
                ├── 📂XR DeviceSimulator
    ├── 📂 Scenes
    ├── 📂 TextMesh Pro
        ├── 📂Documentation
        ├── 📂Fonts
        ├── 📂Rescources
            ├── 📂Fonts & Materials
            ├── 📂Sprite Assets
            ├── 📂Style Sheets
        ├── 📂Shaders
        ├── 📂Sprites
    ├── 📂 XR 
    ├── 📂 XRI 
├── 📂 Library
    ├── 📂APIUpdater
    ├── 📂Artifacts
        ├── 📂256 Folders
    ├── 📂Bee
        ├── 📂artifacts
            ├── 📂1900b0aE.dag
            ├── 📂mvdfrm
        ├── 📂CachedNodeOutput
    ├── 📂PackageCache
        ├── 📂58 Folders
    ├── 📂PackageManager
    ├── 📂ScriptAssemlbies
    ├── 📂Search
    ├── 📂ShaderCache
        ├── 📂builtin
            ├── 📂unity_builtin_extra0000
        ├── 📂shader
            ├── 📂38 Folders
        ├── 📂temp
    ├── 📂StateCache
        ├── 📂LayerSettings
    ├── 📂TempArticats
        ├── 📂Extra
        ├── 📂Primary
├── 📂 Logs
├── 📂 Packages
├── 📂 ProjectSettings
    ├── 📂Packages
        ├── 📂com.unity.probuilder
        ├── 📂com.unity.testtools.codecoverage
├── 📂 Temp
    ├── 📂ProcessJobs
├── 📂 Documentation
    ├── 📂 ContrubutionAndDevelopment.md
    ├── 📂 PersonalContribution.md
├── 📂 UserSettings