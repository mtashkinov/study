module Main

type DayLightType =
    | Morning
    | Noon
    | Afternoon
    | Night

type CreatureType =
    | Puppy
    | Kitten
    | Hedgehog
    | Bearcub
    | Piglet
    | Bat
    | Balloon

type Creature(creatureType : CreatureType) = 
    member x.getType() = creatureType

type ICourier =
    abstract member giveBaby : Creature -> Unit

type IDayLight =
    abstract dayLight : DayLightType

type ILuminary =
    abstract isShining :  bool

type IWind =
    abstract speed : int

type IMagic =
    abstract member callStork : Unit -> ICourier
    abstract member callDaemon : Unit -> ICourier

type Cloud(daylight : IDayLight, luminary : ILuminary, wind : IWind, magic : IMagic) = 

    member private x.InternalCreate() =
      if luminary.isShining then
          match daylight.dayLight with
          | Morning -> let speed = wind.speed 
                       if (speed >= 3) && (speed <= 5) then (Puppy, true)
                                                       else (Piglet, false)
          | Noon -> if wind.speed = 0 then (Kitten, true)
                                      else (Piglet, false)
          | Afternoon -> if wind.speed = 0 then (Balloon, false)
                                           else (Piglet, false)
          | Night -> (Hedgehog, false)
      else
          match daylight.dayLight with
          | Morning -> if wind.speed = 10 then (Bearcub, true)
                                          else (Piglet, false)
          | Noon -> (Piglet, true)
          | Afternoon -> (Piglet, false)
          | Night -> (Bat, false)
 
    member x.Create() =
      let result = x.InternalCreate()
      match result with
      | (creatureType, isCallStork) ->
          let creature = new Creature(creatureType)   
          if isCallStork then magic.callStork().giveBaby(creature)
                         else magic.callDaemon().giveBaby(creature)
          creature