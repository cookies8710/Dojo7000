epsilon = 0.0000000001

#E_1(X)=p1*1+(1-p1-p2)*p1*2 + ...
 #     (0) 1   
#E_2(X)=p2*1+p1*p1*2+(1-p1-p2)*(p1)*p1*3...
      #(0) 2    (0) 1 (0) 1

# E_3(X)= 1 2 + 1 0 2 + 1 0 0 2
def exp_rounds(p1, p2, n):
    suma = 0
    if (n == 1):        
        suma = p1
        pc = 1 - p1 - p2        
        roundCnt = 2
        while(pc > epsilon):
            suma += p1*pc*roundCnt
            pc *= (1 - p1 - p2)
            roundCnt += 1
    elif (n==3):
        suma = p1 * p2
        pc = 1 - p1 - p2        
        roundCnt = 2
        while(pc > epsilon):
            suma += p1*pc*roundCnt*p2
            pc *= (1 - p1 - p2)
            roundCnt += 1
    elif (n==2):
        suma = p2
        pc = 1 - p1 - p2        
        roundCnt = 2
        while(pc > epsilon):
            suma += p2*pc*roundCnt
            pc *= (1 - p1 - p2)
            roundCnt += 1
        suma += p1
        while (pc > epsilon):
            pc *= (1 - p1 - p2)
            while (pc * p1 > epsilon):
                suma += p1 * pc * p1 * pc2
                pc2 *= pc2 * (1-p1 -p2)
                roundCnt2 += 1
            roundCnt += 1
                
    return suma

print(exp_rounds(0.5,0.5,2)+exp_rounds(0.5,0.5,2))
print(exp_rounds(0.5,0.5,2)+exp_rounds(0.5,0.5,1))
print(exp_rounds(0.5,0.5,1)+exp_rounds(0.5,0.5,2))
print(exp_rounds(0.5,0.5,1)+exp_rounds(0.5,0.5,1)+exp_rounds(0.5,0.5,1))

