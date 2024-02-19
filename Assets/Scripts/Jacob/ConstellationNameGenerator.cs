using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationNameGenerator {
    private List<Part> parts = new List<Part>();

    public ConstellationNameGenerator(List<Part> _parts) {
        parts = _parts;
    }

    public string GenerateName() {
        var newName = "";

        if (parts.Count == 1) {
            newName += GetSingle(parts[0]);
        } else if (parts.Count > 0) {
            for (int i = 0; i < parts.Count; i++) {
                if (i == 0) {
                    newName += GetPrefix(parts[i]);
                } else if (i == parts.Count - 1) {
                    newName += GetSuffix(parts[i]);
                } else {
                    newName += GetSyllable(parts[i]);
                }
            }
        } else {
            newName = "N/A";
        }

        return newName;
    }

    private string GetPrefix(Part part) {
        var name = "";

        switch (part.elementType) {
            case ElementType.WATER:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "po";
                        break;
                    case PartType.BIG:
                        name = "posai";
                        break;
                }
                break;
            case ElementType.WIND:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "dinn";
                        break;
                    case PartType.BIG:
                        name = "dinna";
                        break;
                }
                break;
            case ElementType.EARTH:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "as";
                        break;
                    case PartType.BIG:
                        name = "asase";
                        break;
                }
                break;
            case ElementType.FIRE:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "mar";
                        break;
                    case PartType.BIG:
                        name = "marsa";
                        break;
                }
                break;
        }

        return name;
    }

    private string GetSyllable(Part part) {
        var name = "";

        switch (part.elementType) {
            case ElementType.WATER:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "pi";
                        break;
                    case PartType.BIG:
                        name = "pi";
                        break;
                }
                break;
            case ElementType.WIND:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "da";
                        break;
                    case PartType.BIG:
                        name = "dae";
                        break;
                }
                break;
            case ElementType.EARTH:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "a";
                        break;
                    case PartType.BIG:
                        name = "ae";
                        break;
                }
                break;
            case ElementType.FIRE:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "ma";
                        break;
                    case PartType.BIG:
                        name = "mae";
                        break;
                }
                break;
        }

        return name;
    }

    private string GetSuffix(Part part) {
        var name = "";

        switch (part.elementType) {
            case ElementType.WATER:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "son";
                        break;
                    case PartType.BIG:
                        name = "sondon";
                        break;
                }
                break;
            case ElementType.WIND:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "dian";
                        break;
                    case PartType.BIG:
                        name = "dianon";
                        break;
                }
                break;
            case ElementType.EARTH:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "aesir";
                        break;
                    case PartType.BIG:
                        name = "aesiron";
                        break;
                }
                break;
            case ElementType.FIRE:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "maria";
                        break;
                    case PartType.BIG:
                        name = "marion";
                        break;
                }
                break;
        }

        return name;
    }

    private string GetSingle(Part part) {
        var name = "";

        switch (part.elementType) {
            case ElementType.WATER:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "pos";
                        break;
                    case PartType.BIG:
                        name = "posse";
                        break;
                }
                break;
            case ElementType.WIND:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "diats";
                        break;
                    case PartType.BIG:
                        name = "diatsa";
                        break;
                }
                break;
            case ElementType.EARTH:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "aest";
                        break;
                    case PartType.BIG:
                        name = "aestir";
                        break;
                }
                break;
            case ElementType.FIRE:
                switch (part.partType) {
                    case PartType.SMALL:
                        name = "mara";
                        break;
                    case PartType.BIG:
                        name = "marrar";
                        break;
                }
                break;
        }

        return name;
    }
}
