BomSquad
========

Detects Byte-order marks inside C# files as part of MS Build

Use it like this;

    var dog = new BomSquad.SnifferDog();

    // eg, true
    var hasBom = dog.DetectBom(filePath);

    // eg "UTF-16 (LE)"
    var bomType = dog.DetectBomType(filePath);

Available through pre-release nuget package at [https://www.nuget.org/packages/BomSquad/](https://www.nuget.org/packages/BomSquad/). Original source at [https://github.com/stevecooperorg/BomSquad/](https://github.com/stevecooperorg/BomSquad/).
