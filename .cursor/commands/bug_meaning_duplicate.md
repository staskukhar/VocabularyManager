## Integrate logic for handling meaning duplicates when new word are being added.

### Problem flow:
1. User trying to add new words to existing vocabulary which can already contain some words.
2. When a words already exists, it adds only meanings instead of creating new one. (Which is fine) (last commit)
3. When meaning is already present in th word it will result into new duplicate of meaning.

Expected result:
When meaning is being added to a word it must be checked whether exactly the same meaning already present in the word. Which means definition, lexeme, level are the same. In such a case new meanings shouldn't be added. Not error is thrown, just silently skip the meaning and go next.

## Notes:
- All rule sdefined must be followed.
- In case of any detail is missing, stop and ask.
- Update test if needed. Cover new code with tests if some is being added.