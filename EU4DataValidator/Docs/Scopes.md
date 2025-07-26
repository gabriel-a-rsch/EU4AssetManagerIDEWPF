https://eu4.paradoxwikis.com/Scopes
There are a couple of types of scopes in the EU4 Data Annotation.
Ther are 4 dynamic scopes:
ROOT -> country that called the event
FROM -> country that defined the event in their file
PREV -> previous calling scope (usually the parent scope))
THIS -> current calling scope
There are also 3 logic scopes: // irrelevant for this current implementation
AND -> All scopes must be true
OR -> At least one scope must be true
NOT -> Scope must not be true
THERE IS NO IF SCOPE or STATEMENT.
4 Subscopes are used to define the scope of the event: // irrelevant for this current implementation
all_<scope>
any_<scope>
every_<scope>
random_<scope>
There is one special scope that acts as a filter on subscopes that are its parent:
limit = {} // This scope is used to limit the subscopes to a specific set of countries or provinces.
i.e. every_country = {_limit = { stability = 3 } }
There is one special province scope: the trade scope. In order to apply trade effects technically you need to use the trade scope.
i.e. random_active_trade_node = { every_privateering_country }
NOT
ROOT = { every_privateering_country }
