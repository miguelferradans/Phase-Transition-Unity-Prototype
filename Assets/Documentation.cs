
/// <sumary>   29/11/2015   Adrián Jorge Pérez de Muniain
///
///  Changed the comuncation between the Energy.cs and the TransformState.cs to Messages using this methods:
///     TransformState methods:
///         void EnergyTransformation();
///         void EnergyLeft(bool value);
///     Energy methods:
///         void ConsumeEnergy();
///         void RegenEnergy(float amount);
///         void RegenMaxEnergy()
///         
///  Also I've made some variables:
///     TransformState variables:
///         private bool m_canTransform;
///     Energy variables:
///         private float _maxValue;
///         private float _energyUsed;
/// 
/// </sumary>
//
/// <sumary>   30/11/2015   Adrián Jorge Pérez de Muniain
///
///  1  Included the ability to change to any phase from any phase.
///     Now if you press down being water you transform to gas. And
///     if you press up being Gas you transform to Water.
///                   Up       Up     Up
///                 --->     --->   --->
///             Water    Solid    Gas    Water
///                 <---     <---   <---
///                 Down     Down   Down
///     The change of any state to solid does not use energy anymore. To solve the error
///     that happens when you dont have any energy and you are stuck being water or gas.
///     
///  2  The GameObject DoorTrigger now contains a new script:
///         -GoNextLevel.cs
///     
///  3  Added a new GameObject:
///         -GameManager
///             -SpawnPointPlayer
///             -SpawnPointFinalDoor
///     The GameManager has two new Scripts:
///         -InformationLevel.cs
///         -RestarLevel.cs
///     
///  4  Two new scenes have been added:
///         -start
///         -level2
///     The start scene contains buttons that allow you to go to the different levels that
///     have been made.
///     The level2 scene is a new level.
/// 
///  5  The buttons of the start scene have a new script:
///         -LoadLevel.cs
/// 
///  6  New forldes have been added with the new content:
///         Prefabs:
///             Dinamic Elements:
///                 -Platforme
///                 -Doorkey
///                 -DoorLever
///             Interact Elements:
///                 -Key
///                 -LeverForDoors
///                 -LeverForPlatfomrs
///             Managers:
///                 -GameManager
///             Player:
///                 -Canvas
///                 -Player
///         Scripts:
///             ButtonsScript:
///                 -LoadLevel
///             GameManagers:
///                 -InformationLevel
///                 -RestartLevel
///             Interactuable Elements:
///                 -GoNextLevel
///                 -MovingPlatform
///                 -OpenDoor
///             Player:
///                 -Energy
///                 -Interactuar
///                 -PlayerMovement
///                 -TransformState
/// </sumary>


