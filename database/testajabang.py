import random
def to_alay(name):
    transformations = {
        'a': '4', 'A': '4',
        'b': '8', 'B': '8',
        'c': '(', 'C': '(',
        'e': '3', 'E': '3',
        'g': '6', 'G': '6',
        'i': '1', 'I': '1',
        'l': '1', 'L': '1',
        'o': '0', 'O': '0',
        's': '5', 'S': '5',
        't': '7', 'T': '7',
        'z': '2', 'Z': '2'
    }
    
    # Randomly remove some letters (but not spaces)
    result = []
    for char in name:
        if char != ' ' and random.random() < 0.5:
            char = transformations.get(char,char)
            
        if char != ' ' and random.random() < 0.1:  # 10% chance to remove a character
            continue
        
        result.append(char)
    
    return ''.join(result)


nametest = "matthew"

for i in range(10):
    print(to_alay(nametest))