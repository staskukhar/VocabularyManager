# Feature - integration of word's meanings

## Plan

- Update schemas. Add table for Meanings. Configure relations one to many. One word can have multiple meanings. Lexema Type, Definition, Level should be moved to Meaning Entity.

- Analyze the logic of Client end define how endpoints, models, DTOs, validators, etc. must be changed.

- Finally adapt Client app.

## Requirements & User stories

- When a word is deleted, the meanings are gone too.

- User must be able to add, edit, delete and update meanings, the same for words

- A word can exist without any meanings, a meaning can't without a word

## Notes

- Correct mistakes like 'Defenition' column name. Any other related to schemas if exist.

- Add details to columns when needed. For example max length.

- Word name must be unique.

- String columns must have length limits.

- Move entity configurations from context to separate classes