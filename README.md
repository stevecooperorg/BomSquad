BomSquad
========

Detects Byte-order marks inside C# files as part of MS Build

Use it like this;

    var dog = new SnifferDog();

    // eg, true
    var hasBom = dog.DetectBom(filePath);

    // eg "UTF-16 (LE)"
    var bomType = dog.DetectBomType(filePath);
