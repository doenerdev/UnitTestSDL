# Code Coverage Metrics

## Line Coverage

Example 1:

    //prod 60%
    f(a, b)
        if a > b  
            print(a)  
        else  
            print(b)
    
    //test
    test -> f(10, 5)

Example 2:

    //prod 100%
    f(a, b)
        a > b ? print(a) : print(b)
    
    //test
    test -> f(10, 5)
    

## Statement Coverage

    //prod 75%
    f(a, b)
        a > b ? print(a) : print(b)

    //test
    test -> f(10, 5)

## Branch Coverage

    //prod 50%
    f(a, b)
        if a > b  
            print(a)  
        else  
            print(b)
    
    //test
    test -> f(10, 5)

## Statement & Line Coverage vs Branch Coverage

    //line & statement = 100% / branch coverage = 50%
    f(a, b)
        result = b
        if a > b  
            result = a 
        print(b)
    
    //test
    test -> f(10, 5)

## Condition Coverage / Compound Condition Coverage

    //prod 33%
    f(a, b)
        if a > b && a > 0  
            print(a)  
        else  
            print(b)
    
    //test
    test -> f(-10, -500)

## Function Coverage, Loop Coverage etc...

e.g. see https://www.atlassian.com/continuous-delivery/software-testing/code-coverage
