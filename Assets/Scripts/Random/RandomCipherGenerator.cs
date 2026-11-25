using Assets.Scripts.Cyphers;
using Assets.Scripts.Save;
using System;
using System.Collections.Generic;
using System.Linq;

public class RandomCipherGenerator
{
    public static int MaxWordLength => Words[Localization.RU].Max().Length;

    public static Dictionary<Localization, string[]> Words = new Dictionary<Localization, string[]>
    {
        {Localization.RU,
            new string[]{
                "дом", "кот", "собака", "мир", "солнце", "луна", "звезда", "река", "лес", "гора",
                "вода", "дождь", "снег", "ветер", "мост", "путь", "книга", "стол", "стул", "кошка",
                "птица", "рыба", "молоко", "хлеб", "яблоко", "груша", "апельсин", "банан", "цвет", "свет",
                "тень", "песок", "камень", "блок", "флаг", "город", "село", "долина", "дуб", "ель",
                "листва", "трава", "птицы", "туман", "дым", "сила", "душа", "спас", "праздник", "друг",
                "любовь", "истина", "сказка", "музыка", "глаз", "улыбка", "песня", "звон", "восторг", "забава",
                "игра", "весна", "осень", "лето", "зима", "первый", "второй", "третий", "четвёртый", "пятый",
                "шестой", "седьмой", "восьмой", "девятый", "десятый", "событие", "новый", "старый", "жизнь", "счастье",
                "долг", "небо", "земля", "огонь", "воздух", "берег", "волна", "остров", "поле", "сад",
                "дерево", "куст", "цветок", "роза", "лилия", "мак", "клён", "берёза", "сосна", "ива",
                "ягода", "гриб", "мёд", "соль", "сахар", "чай", "кофе", "суп", "каша", "сыр",
                "масло", "мясо", "рыбак", "повар", "врач", "учитель", "школа", "урок", "класс", "парта",
                "доска", "мел", "ручка", "тетрадь", "бумага", "письмо", "слово", "буква", "звук", "голос",
                "шум", "тишина", "покой", "сон", "мечта", "надежда", "вера", "правда", "ложь", "тайна",
                "ключ", "дверь", "окно", "стена", "крыша", "пол", "потолок", "лестница", "коридор", "комната",
                "кухня", "спальня", "кровать", "шкаф", "полка", "лампа", "свеча", "зеркало", "часы", "ковёр",
                "подушка", "одеяло", "простыня", "занавес", "шкатулка", "коробка", "сумка", "рюкзак", "чемодан", "кошелёк",
                "деньги", "монета", "купюра", "карта", "билет", "паспорт", "фото", "картина", "рамка", "кисть",
                "краска", "холст", "мольберт", "скрипка", "гитара", "пианино", "флейта", "барабан", "труба", "нота",
                "ритм", "танец", "сцена", "театр", "кино", "актёр", "зритель", "роль", "маска", "костюм",
                "платье", "рубашка", "брюки", "куртка", "пальто", "шапка", "шарф", "перчатка", "носок", "ботинок",
                "сапог", "туфля", "ремень", "пуговица", "карман", "рукав", "воротник", "молния", "нитка", "игла",
                "ткань", "шёлк", "лен", "хлопок", "шерсть", "кожа", "мех", "пух", "перо", "крыло",
                "хвост", "лапа", "нос", "ухо", "рот", "губа", "зуб", "язык", "щека", "лоб",
                "бровь", "ресница", "волос", "палец", "ладонь", "локоть", "плечо", "спина", "грудь", "живот",
                "нога", "колено", "пятка", "сердце", "лёгкое", "печень", "кровь", "кость", "мышца", "нерв",
                "мозг", "память", "разум", "мысль", "идея", "план", "цель", "шаг", "бег", "прыжок",
                "полёт", "падение", "подъём", "спуск", "поворот", "круг", "квадрат", "треугольник", "линия", "точка",
                "угол", "центр", "край", "начало", "конец", "середина", "верх", "низ", "право", "лево",
                "близко", "далеко", "высоко", "низко", "быстро", "медленно", "тихо", "громко", "тепло", "холодно",
                "ярко", "тускло", "сладко", "горько", "кисло", "солёно", "остро", "мягко", "твёрдо", "гладко",
                "шершаво", "легко", "тяжело", "просто", "сложно", "ясно", "мутно", "чисто", "грязно", "пусто",
                "полно", "густо", "редко", "часто", "всегда", "никогда", "иногда", "снова", "опять", "сразу"
            }
        },
        {Localization.EN,
            new string[]{
                "house", "cat", "dog", "world", "sun", "moon", "star", "river", "forest", "mountain",
                "water", "rain", "snow", "wind", "bridge", "path", "book", "table", "chair", "bird",
                "fish", "milk", "bread", "apple", "pear", "orange", "banana", "color", "light", "shadow",
                "sand", "stone", "block", "flag", "city", "village", "valley", "oak", "fir", "foliage",
                "grass", "birds", "fog", "smoke", "strength", "soul", "saved", "holiday", "friend", "love",
                "truth", "fairy", "music", "eye", "smile", "song", "ring", "delight", "game", "spring",
                "autumn", "summer", "winter", "first", "second", "third", "fourth", "fifth", "sixth", "seventh",
                "eighth", "ninth", "tenth", "event", "new", "old", "life", "happiness", "debt", "sky",
                "earth", "fire", "air", "shore", "wave", "island", "field", "garden", "tree", "bush",
                "flower", "rose", "lily", "poppy", "maple", "birch", "pine", "willow", "berry", "mushroom",
                "honey", "salt", "sugar", "tea", "coffee", "soup", "porridge", "cheese", "butter", "meat",
                "fisher", "cook", "doctor", "teacher", "school", "lesson", "class", "desk", "board", "chalk",
                "pen", "notebook", "paper", "letter", "word", "sound", "voice", "noise", "silence", "rest",
                "sleep", "dream", "hope", "faith", "lie", "secret", "key", "door", "window", "wall",
                "roof", "floor", "ceiling", "stairs", "corridor", "room", "kitchen", "bedroom", "bed", "wardrobe",
                "shelf", "lamp", "candle", "mirror", "clock", "carpet", "pillow", "blanket", "sheet", "curtain",
                "box", "bag", "backpack", "suitcase", "wallet", "money", "coin", "note", "card", "ticket",
                "passport", "photo", "picture", "frame", "brush", "paint", "canvas", "easel", "violin", "guitar",
                "piano", "flute", "drum", "trumpet", "note", "rhythm", "dance", "stage", "theater", "cinema",
                "actor", "viewer", "role", "mask", "costume", "dress", "shirt", "trousers", "jacket", "coat",
                "hat", "scarf", "glove", "sock", "boot", "shoe", "belt", "button", "pocket", "sleeve",
                "collar", "zipper", "thread", "needle", "cloth", "silk", "linen", "cotton", "wool", "leather",
                "fur", "fluff", "feather", "wing", "tail", "paw", "nose", "ear", "mouth", "lip",
                "tooth", "tongue", "cheek", "forehead", "brow", "lash", "hair", "finger", "palm", "elbow",
                "shoulder", "back", "chest", "belly", "leg", "knee", "heel", "heart", "lung", "liver",
                "blood", "bone", "muscle", "nerve", "brain", "memory", "mind", "thought", "idea", "plan",
                "goal", "step", "run", "jump", "flight", "fall", "rise", "descent", "turn", "circle",
                "square", "triangle", "line", "point", "angle", "center", "edge", "start", "end", "middle",
                "top", "bottom", "right", "left", "near", "far", "high", "low", "fast", "slow",
                "quiet", "loud", "warm", "cold", "bright", "dim", "sweet", "bitter", "sour", "salty",
                "spicy", "soft", "hard", "smooth", "rough", "easy", "heavy", "simple", "complex", "clear",
                "muddy", "clean", "dirty", "empty", "full", "thick", "rare", "often", "always", "never",
                "sometimes", "again", "immediately", "slowly", "quickly", "carefully", "boldly", "silently", "loudly", "gently"
            }
        }
    };

    public static Cipher GetRandomCipher(GameConfig config)
    {
        int enumLen = Enum.GetValues(typeof(CipherType)).Length;
        CipherType type;
        Localization localization = GameSaves.Instance.Localization;
        Alphabet alphabet = AlphabetManager.GetAlphabetType(localization);
        int shift;

        string message = "", key = "";

        do
        {
            type = (CipherType)UnityEngine.Random.Range(0, 6);
            message = GetRandomWord();
            key = GetRandomWord();

            if (type == CipherType.SkitalaCipher)
                shift = UnityEngine.Random.Range(0, 5);
            else
                shift = UnityEngine.Random.Range(1, AlphabetManager.GetAlphabet(localization).Length);

        }
        while (message.Length < key.Length || !config.IsEnabled(type));

        switch (type)
        {
            case CipherType.CaesarWithKeyCipher:
                return new CaesarsWithKeyСipher(message, alphabet, key);
            case CipherType.CaesarCipher:
                return new CaesarsСipher(message, alphabet, shift);
            case CipherType.LadderCipher:
                return new LadderCipher(message);
            case CipherType.ReverseCipher:
                return new ReverseCipher(message);
            case CipherType.SkitalaCipher:
                return new SkitalaCipher(message, alphabet, shift);
            case CipherType.VigenereCipher:
                return new VigenereCipher(message, alphabet, key);
            default:
                throw new ArgumentException();
        }
    }

    public static string GetRandomWord()
    {
        var localizedWords = Words[GameSaves.Instance.Localization];
        return localizedWords[UnityEngine.Random.Range(0, localizedWords.Length)];
    }
}
