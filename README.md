BomSquad
========

Detects Byte-order marks inside C# files as part of MS Build

Use it like this;

    var dog = new BomSquad.SnifferDog();

    // eg, true
    var hasBom = dog.DetectBom(filePath);

    // eg "UTF-16 (LE)"
    var bomType = dog.DetectBomType(filePath);
